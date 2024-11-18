using MediatR;
using PetWorldOficial.Application.ViewModels.Schedule;

namespace PetWorldOficial.Application.Queries.Schedule;

public class GetSchedulingByCodeQuery : IRequest<IEnumerable<ScheduleDetailsViewModel>>
{
    public Guid Code { get; set; }
}