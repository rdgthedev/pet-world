using MediatR;

namespace PetWorldOficial.Application.Commands.Scheduling;

public class UpdateStatusFinishedCommand : IRequest<(int statusCode, string message)>
{
    public Guid Code { get; set; }
}