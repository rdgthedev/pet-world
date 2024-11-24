using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class UserRepository(
    UserManager<User> userManager,
    RoleManager<Role> roleManager) : IUserRepository
{
    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
        => await userManager
            .Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await userManager
            .Users
            .Include(u => u.Schedullings)
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);

    public async Task<IEnumerable<User>> GetAllUsersExceptCurrentAsync(int userId, CancellationToken cancellationToken)
        => await userManager
            .Users
            .AsNoTracking()
            .Where(u => u.Id != userId)
            .ToListAsync(cancellationToken);

    public async Task UpdatePasswordAsync(User user, string currentPassword, string newPassword)
    {
        var result = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (!result.Succeeded)
            throw new UnableToChangePasswordException("Erro ao tentar alterar a senha");
    }


    public async Task<IEnumerable<User?>> GetUsersByRoleAsync(ERole roleName)
    {
        return await userManager.GetUsersInRoleAsync(roleName.ToString());
    }

    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
        => await userManager
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.UserName == userName, cancellationToken);

    public async Task<User?> GetByEmailAsync(string email)
        => await userManager.FindByEmailAsync(email);

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
        => await userManager
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber, cancellationToken);

    public async Task<User?> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
        => await userManager
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Document == cpf, cancellationToken);

    public async Task<User?> GetByDocumentAsync(string document, CancellationToken cancellationToken)
        => await userManager
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Document == document, cancellationToken);

    public async Task UpdateAsync(User entity)
        => await userManager.UpdateAsync(entity);

    public async Task DeleteAsync(User entity)
        => await userManager.DeleteAsync(entity);
}