using MediatR;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Services.Interfaces;

namespace PetWorldOficial.Application.Handlers.User;

public class DeleteUserCommandHandler(
    IUserService userService) : IRequestHandler<DeleteUserCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await userService.DeleteAsync(request, cancellationToken);
            return Unit.Value;
        }
        catch (Exception)
        {
            throw;
        }
    }
}