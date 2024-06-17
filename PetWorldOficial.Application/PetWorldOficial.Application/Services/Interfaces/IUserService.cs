using PetWorldOficial.Application.ViewModels.User;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<UserDetailsViewModel>> GetAll();
    Task<User?> GetById(int id);
    Task<UserDetailsViewModel?> GetByUserName(string userName);
    Task<bool> UserExists(User user);
    Task Update(UpdateUserViewModel model);
    Task Delete(DeleteUserViewModel model);
}