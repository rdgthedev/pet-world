using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class UserRepository(UserManager<User> _userManager) : IUserRepository
{
    public async Task<IEnumerable<User>> GetAllAsync()
        => await _userManager.Users.AsNoTracking().ToListAsync();

    public async Task<User?> GetByIdAsync(int id)
        => await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id);

    public async Task<User?> GetByUserNameAsync(string userName)
        => await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(user => user.UserName == userName);

    public async Task<User?> GetByEmailAsync(string email)
        => await _userManager.FindByEmailAsync(email);

    public async Task<User?> GetByPhoneNumberAsync(string phoneNumber)
        => await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);

    public async Task<User?> GetByDocumentAsync(string document)
        => await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Document == document);

    public async Task UpdateAsync(User entity)
        => await _userManager.UpdateAsync(entity);

    public async Task DeleteAsync(User entity)
        => await _userManager.DeleteAsync(entity);
}