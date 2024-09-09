using System.Security.Claims;
using MediatR;
using PetWorldOficial.Application.ViewModels.Schedule;

namespace PetWorldOficial.Application.Queries.Schedule;

public record GetAllSchedulesQuery(ClaimsPrincipal UserPrincipal)
    : IRequest<IEnumerable<ScheduleDetailsViewModel>>
{
}