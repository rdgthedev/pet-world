﻿using PetWorldOficial.Application.Commands.User;
using PetWorldOficial.Application.ViewModels.User;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDetailsViewModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<UserDetailsViewModel> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<UserDetailsViewModel?> GetByUserNameAsync(string userName, CancellationToken cancellationToken);
    Task<UserDetailsViewModel?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<UserExistsViewModel> UserExistsAsync(
        string? cpf,
        string? phoneNumber,
        string? email,
        CancellationToken cancellationToken);

    Task UpdateAsync(UpdateUserCommand command, CancellationToken cancellationToken, bool isAdmin = false);
    Task DeleteAsync(DeleteUserCommand command, CancellationToken cancellationToken);
    Task UpdatePasswordAsync(MyAccountCommand command);

    Task<IEnumerable<UserDetailsViewModel>> GetAllUsersExceptCurrentAsync(
        int userId,
        CancellationToken cancellationToken);

    Task<int> CountUsersByRoleAsync(ERole roleName);

    Task<IEnumerable<UserDetailsViewModel>> GetUsersByRoleAsync(
        ERole role,
        CancellationToken cancellationToken);
}