using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PetWorldOficial.Application.Queries.User;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.User;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.User;

public class GetAllClientsAndEmployesQueryHandler(
    IUserService userService,
    UserManager<Domain.Entities.User> userManager,
    IMapper mapper) : IRequestHandler<GetAllClientsAndEmployeesQuery, IEnumerable<UserDetailsViewModel>>
{
    public async Task<IEnumerable<UserDetailsViewModel>> Handle(
        GetAllClientsAndEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var email = request.UserPrincipal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            var userResult = await userService.GetByEmailAsync(
                email!,
                cancellationToken);

            if (userResult is null)
                throw new AccessFailureException(
                    "Falha ao acessar. Tente recarregar a página ou fazer o Login novamente!");

            var usersDetails = await userService.GetAllUsersExceptCurrentAsync(userResult.Id, cancellationToken);

            if (!usersDetails.Any())
                throw new UserNotFoundException("Nenhum usuário encontrado!");

            var users = usersDetails
                .Select(mapper.Map<Domain.Entities.User>)
                .ToList();

            foreach (var user in users)
            {
                var userDetails = usersDetails.ToList().FirstOrDefault(u => u.Id == user.Id);
                var role = Enum.Parse<ERole>((await userManager.GetRolesAsync(user)).FirstOrDefault()!);
                userDetails!.RoleName = role is ERole.Admin ? "Administrador" : "Cliente";
            }
            return usersDetails.OrderBy(u => u.RoleName);
        }
        catch (Exception)
        {
            throw;
        }
    }
}