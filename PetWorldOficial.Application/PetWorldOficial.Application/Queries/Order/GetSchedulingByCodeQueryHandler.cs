using AutoMapper;
using MediatR;
using PetWorldOficial.Application.Queries.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Schedule;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Queries.Order;

public class GetSchedulingByCodeQueryHandler(
    IScheduleService schedulingService,
    IMapper mapper) : IRequestHandler<GetSchedulingByCodeQuery, IEnumerable<ScheduleDetailsViewModel>>
{
    public async Task<IEnumerable<ScheduleDetailsViewModel>> Handle(GetSchedulingByCodeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var scheduling = await schedulingService.GetAllByCode(request.Code, cancellationToken);
            
            if (scheduling is null)
                throw new ScheduleNotFoundException("Agendamento não encontrado!");

            return scheduling;
        }
        catch (Exception)
        {
            throw;
        }
    }
}