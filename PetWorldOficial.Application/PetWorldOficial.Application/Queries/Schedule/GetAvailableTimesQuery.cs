using System.Security.Claims;
using MediatR;
using PetWorldOficial.Application.DTO;

namespace PetWorldOficial.Application.Queries.Schedule;

public record GetAvailableTimesQuery(
    DateTime Date,
    int DurationInMinutes,
    string ServiceName,
    string CategoryName,
    ClaimsPrincipal User) : IRequest<List<TimeDTO>>;