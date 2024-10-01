using System.Security.Claims;
using MediatR;

namespace PetWorldOficial.Application.Queries.Schedule;

public record GetAvailableTimesQuery(
    DateTime Date, 
    int DurationInMinutes,
    string ServiceName,
    ClaimsPrincipal User) : IRequest<IEnumerable<TimeSpan>>;