using AutoMapper;
using MediatR;
using PetWorldOficial.Application.Commands.Scheduling;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Scheduling;

public class UpdateStatusToFinishedCommandHandler(
    IScheduleService schedulingService,
    IMapper mapper) : IRequestHandler<UpdateStatusFinishedCommand, (int statusCode, string message)>
{
    public async Task<(int statusCode, string message)> Handle(
        UpdateStatusFinishedCommand request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var schedulingDetailsViewModel = await schedulingService.GetAllByCode(request.Code, cancellationToken);

            if (schedulingDetailsViewModel is null)
                throw new ScheduleNotFoundException("Agendamento não encontrado!");

            var schedulings = mapper.Map<IEnumerable<Schedulling>>(schedulingDetailsViewModel);
            schedulings.ToList().ForEach(s => s.UpdateStatusToFinished());

            await schedulingService.UpdateRange(mapper.Map<List<UpdateSchedulingCommand>>(schedulings), cancellationToken);

            return (200, $"O agendamento de número {schedulings.First().Id} foi finalizado com sucesso!");
        }
        catch (Exception)
        {
            throw;
        }
    }
}