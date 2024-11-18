using MediatR;

namespace PetWorldOficial.Application.Commands.Scheduling;

public class UpdateStatusToCanceledCommand : IRequest<(int statusCode, string message)>
{
    public Guid Code { get; set; }
}