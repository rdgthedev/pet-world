using MediatR;
using PetWorldOficial.Application.ViewModels.User;

namespace PetWorldOficial.Application.Queries.Schedule;

public record GetAvailaEmployeesToSchedulingQuery(
    string CategoryName,
    string ServiceName,
    DateTime SchedulingDate,
    TimeSpan SchedulingTime,
    int DurationInMinutes) : IRequest<List<(int Id, string Name, bool Status)>>;