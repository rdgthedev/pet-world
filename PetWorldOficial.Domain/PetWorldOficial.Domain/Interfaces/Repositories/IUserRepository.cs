﻿using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);
    Task<User?> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetAllUsersExceptCurrentAsync(int userId, CancellationToken cancellationToken);
    Task<IEnumerable<User?>> GetUsersByRoleAsync(ERole roleName);
    Task<bool> RemoveFromRolesAsync(User user, string currentRole);
    Task<bool> AddRoleToUserAsync(User user, string newRole);
    Task UpdatePasswordAsync(
        User user, 
        string currentPassword, 
        string newPassword);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
}