using AutoMapper;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.User;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class UserService(
    IUserRepository _userRepository,
    IMapper _mapper) : IUserService
{
    public async Task<IEnumerable<UserDetailsViewModel>> GetAll()
        => _mapper.Map<IEnumerable<UserDetailsViewModel>>(await _userRepository.GetAllAsync());

    public async Task<User?> GetById(int id)
        => await _userRepository.GetByIdAsync(id);

    public async Task<UserDetailsViewModel?> GetByUserName(string? userName)
        => userName is null
            ? null
            : _mapper.Map<UserDetailsViewModel>(await _userRepository.GetByUserNameAsync(userName));

    public async Task<bool> UserExists(User user)
    {
        var searchTasks = new List<User?>
        {
            await _userRepository.GetByUserNameAsync(user.UserName!),
            await _userRepository.GetByDocumentAsync(user.Document),
            await _userRepository.GetByPhoneNumberAsync(user.PhoneNumber!),
            await _userRepository.GetByEmailAsync(user.Email!)
        };

        return searchTasks.Any(u => u != null);
    }

    public async Task Update(UpdateUserViewModel model)
    {
        var existingUser = await _userRepository.GetByIdAsync(model.Id);

        if (existingUser is null)
            throw new UserNotFoundException("Usuário não encontrado!");

        await _userRepository.UpdateAsync(_mapper.Map(model, existingUser));
    }

    public async Task Delete(DeleteUserViewModel model)
    {
        var existingUser = await _userRepository.GetByIdAsync(model.Id);

        if (existingUser is null)
            throw new UserNotFoundException("Usuário não encontrado!");

        await _userRepository.DeleteAsync(_mapper.Map(model, existingUser));
    }
}