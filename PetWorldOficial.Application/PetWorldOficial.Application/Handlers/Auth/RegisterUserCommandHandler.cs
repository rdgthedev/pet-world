using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Services.Interfaces;

namespace PetWorldOficial.Application.Handlers.Auth;

public class RegisterUserCommandHandler(
    IAuthService authService,
    IUserService userService,
    IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<RegisterUserCommand, List<string>>
{
    public async Task<List<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var errors = new List<string>();

            var userExists = await userService.UserExistsAsync(
                request.Document,
                request.PhoneNumber,
                request.Email,
                cancellationToken);

            if (userExists.Exists)
            {
                errors.Add(userExists.Error);
                return errors;
            }

            var user = mapper.Map<Domain.Entities.User>(request);

            var result = await authService.Register(user, request.Role, request.Password);

            if ((bool)result?.Errors.Any())
            {
                var identityErrors = result.Errors.Select(e => e.Description).ToList();
                errors.AddRange(identityErrors);

                return errors;
            }

            if (!(bool)httpContextAccessor.HttpContext.User.Identity?.IsAuthenticated)
                await authService.SignIn(user);

            return errors;
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void AddModelError(string key, string errorMessage)
    {
        var context = httpContextAccessor.HttpContext;

        if (!context.Items.TryGetValue("ModelState", out var value))
            throw new Exception();

        var modelState = (ModelStateDictionary)value;
        modelState.AddModelError(key, errorMessage);
    }
}