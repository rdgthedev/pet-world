using MediatR;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Services.Interfaces;

namespace PetWorldOficial.Application.Handlers.User;

public class UpdateUserCommandHandler(IUserService userService) : IRequestHandler<UpdateUserCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await userService.UpdateAsync(request, cancellationToken);
            return Unit.Value;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}