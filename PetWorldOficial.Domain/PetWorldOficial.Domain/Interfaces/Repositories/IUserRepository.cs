using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByUserNameAsync(string userName);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByPhoneNumberAsync(string phoneNumber);
    Task<User?> GetByDocumentAsync(string document);
}