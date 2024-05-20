using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByUserName(string userName);
    Task<User?> GetByEmail(string email);
    Task<User?> GetByPhoneNumber(string phoneNumber);
    Task<User?> GetByDocument(string document);
}