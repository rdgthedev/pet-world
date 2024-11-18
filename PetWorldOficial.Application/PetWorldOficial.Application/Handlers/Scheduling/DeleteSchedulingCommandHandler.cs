using MediatR;
using PetWorldOficial.Application.Commands.Scheduling;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Utils;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Scheduling
{
    public class DeleteSchedulingCommandHandler(
        IScheduleService scheduleService) : IRequestHandler<DeleteSchedulingCommand, DeleteSchedulingCommand>
    {
        public async Task<DeleteSchedulingCommand> Handle(DeleteSchedulingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.AnimalName))
                {
                    var scheduling = await scheduleService.GetById(request.SchedulingId, cancellationToken);

                    if (scheduling is null)
                        throw new ScheduleNotFoundException("Agendamento não encontrado!");

                    request.AnimalName = scheduling.Animal.Name;
                    request.ServiceName = scheduling.Service.Name;
                    request.ServicePrice = scheduling.Service.Price;
                    request.EmployeeName = scheduling.Employee.Name;
                    request.DurationInMinutes = scheduling.Service.DurationInMinutes;
                    request.Observation = scheduling.Observation;
                    request.SchedulingDate = scheduling.Date;
                    request.SchedulingTime = scheduling.Time;

                    return request;
                }

                var schedulings = AddSchedulings(request);
                await scheduleService.DeleteRange(schedulings, cancellationToken);

                request.Message = "Agendamento cancelado com sucesso!";
                return request;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private List<DeleteSchedulingCommand> AddSchedulings(DeleteSchedulingCommand request)
        {
            var schedulings = new List<DeleteSchedulingCommand>();

            if (!Validation.IsDurationOneHour(request.DurationInMinutes))
            {
                schedulings.Add(request);
            }
            else
            {
                schedulings.Add(request);
                schedulings.Add(new DeleteSchedulingCommand(request.SchedulingId + 1));
            }

            return schedulings;
        }
    }
}
