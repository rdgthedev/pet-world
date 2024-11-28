using MediatR;
using Microsoft.AspNetCore.Http;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.Handlers.User;

public class UpdateUserCommandHandler(
    IUserService userService,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<UpdateUserCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var context = httpContextAccessor.HttpContext;
            var isAdmin = context.User.IsInRole(ERole.Admin.ToString());
            await userService.UpdateAsync(request, cancellationToken, isAdmin);
            return Unit.Value;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}