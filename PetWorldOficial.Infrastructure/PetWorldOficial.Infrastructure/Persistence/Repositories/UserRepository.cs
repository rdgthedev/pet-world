using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;
    
    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userManager.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Id == id);
    }
    
    public async Task<User?> GetByUserName(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<User?> GetByPhoneNumber(string phoneNumber)
    {
        return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
    }

    public async Task<User?> GetByDocument(string document)
    {
        return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Document == document);
    }
    public async Task Update(User entity)
    {
        await _userManager.UpdateAsync(entity);
    }

    public async Task Delete(User entity)
    {
        await _userManager.DeleteAsync(entity);
    }
}