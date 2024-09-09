using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class UserRepository(UserManager<User> _userManager) : IUserRepository
{
    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
        => await _userManager
            .Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await _userManager
            .Users
            .FirstOrDefaultAsync(user => user.Id == id, cancellationToken);

    public async Task<IEnumerable<User?>> GetUsersByRoleAsync(ERole roleName)
    {
        return await _userManager.GetUsersInRoleAsync(roleName.ToString());
    }

    public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
        => await _userManager
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.UserName == userName, cancellationToken);

    public async Task<User?> GetByEmailAsync(string email)
        => await _userManager.FindByEmailAsync(email);

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken)
        => await _userManager
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber, cancellationToken);

    public async Task<User?> GetByCpfAsync(string cpf, CancellationToken cancellationToken)
        => await _userManager
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Document == cpf, cancellationToken);

    public async Task<User?> GetByDocumentAsync(string document, CancellationToken cancellationToken)
        => await _userManager
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Document == document, cancellationToken);

    public async Task UpdateAsync(User entity)
        => await _userManager.UpdateAsync(entity);

    public async Task DeleteAsync(User entity)
        => await _userManager.DeleteAsync(entity);
}