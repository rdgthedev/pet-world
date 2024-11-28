using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.User;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class UserService(
    IUserRepository userRepository,
    IScheduleRepository schedulingRepository,
    IRoleService roleService,
    IMapper mapper) : IUserService
{
    public async Task<IEnumerable<UserDetailsViewModel>> GetAllAsync(CancellationToken cancellationToken)
        => mapper.Map<IEnumerable<UserDetailsViewModel>>(await userRepository.GetAllAsync(cancellationToken));

    public async Task<UserDetailsViewModel> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(id, cancellationToken);

        if (user is null)
            throw new UserNotFoundException("Usuário não encontrado!");

        return mapper.Map<UserDetailsViewModel>(user);
    }

    public async Task<UserDetailsViewModel?> GetByUserNameAsync(string? userName, CancellationToken cancellationToken)
    {
        return mapper.Map<UserDetailsViewModel>(await userRepository.GetByUserNameAsync(userName!, cancellationToken));
    }

    public async Task<UserDetailsViewModel?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return mapper.Map<UserDetailsViewModel>(await userRepository.GetByEmailAsync(email));
    }

    public async Task<UserExistsViewModel> UserExistsAsync(
        string? cpf,
        string? phoneNumber,
        string? email,
        CancellationToken cancellationToken)
    {
        User? user;

        if (!string.IsNullOrEmpty(cpf))
        {
            user = await userRepository.GetByCpfAsync(cpf, cancellationToken);

            if (user != null)
                return new UserExistsViewModel(true, "Cpf já cadastrado!");
        }

        if (!string.IsNullOrEmpty(phoneNumber))
        {
            user = await userRepository.GetByPhoneNumberAsync(phoneNumber, cancellationToken);

            if (user != null)
                return new UserExistsViewModel(true, "Telefone já cadastrado!");
        }

        if (!string.IsNullOrEmpty(email))
        {
            user = await userRepository.GetByEmailAsync(email);

            if (user != null)
                return new UserExistsViewModel(true, "Email já cadastrado!");
        }

        return new UserExistsViewModel(false, null!);
    }

    public async Task UpdateAsync(
        UpdateUserCommand command,
        CancellationToken cancellationToken,
        bool isAdmin)
    {
        var user = await userRepository.GetByIdAsync(command.Id, cancellationToken);

        if (user is null)
            throw new UserNotFoundException("Usuário não encontrado!");

        var userUpdated = mapper.Map(command, user);

        var roles = await roleService.GetRolesByUserAsync(user);

        await userRepository.UpdateAsync(userUpdated);

        if (isAdmin)
        {
            if (!roles.Any())
                throw new Exception("Nenhum perfil encontrado!");

            var roleDeleted = await userRepository.RemoveFromRolesAsync(user, roles.First());

            if (!roleDeleted)
                throw new Exception("Não foi possível alterar o perfil do usuário!");

            await userRepository.AddRoleToUserAsync(user, command.RoleName);
        }
    }

    public async Task DeleteAsync(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(command.Id, cancellationToken);

        if (user is null)
            throw new UserNotFoundException("Usuário não encontrado!");

        if (user.Schedullings.Any())
            await schedulingRepository.DeleteRangeAsync(user.Schedullings, cancellationToken);

        await userRepository.DeleteAsync(user);
    }

    public async Task UpdatePasswordAsync(MyAccountCommand command)
    {
        var user = await userRepository.GetByEmailAsync(command.Email);

        if (user is null)
            throw new UserNotFoundException("Usuário não encontrado!");

        await userRepository.UpdatePasswordAsync(user, command.Password!, command.NewPassword!);
    }

    public async Task<IEnumerable<UserDetailsViewModel>> GetAllUsersExceptCurrentAsync(
        int userId,
        CancellationToken cancellationToken)
        => mapper.Map<IEnumerable<UserDetailsViewModel>>(
            await userRepository.GetAllUsersExceptCurrentAsync(userId, cancellationToken));

    public async Task<int> CountUsersByRoleAsync(ERole roleName)
    {
        var users = await userRepository.GetUsersByRoleAsync(roleName);

        return users.Count();
    }

    public async Task<IEnumerable<UserDetailsViewModel>> GetUsersByRoleAsync(
        ERole role,
        CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<UserDetailsViewModel>>(await userRepository.GetUsersByRoleAsync(role));
    }
}