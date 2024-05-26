using AutoMapper;
using PetWorldOficial.Application.DTOs.User.Output;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<OutputUserDto>> GetAll()
    {
        var users = _mapper.Map<IEnumerable<OutputUserDto>>(await _userRepository.GetAllAsync());
        if (users == null) throw new NotFoundException("Nenhum usuário encontrado!");
        return users;
    }

    public async Task<OutputUserDto> GetById(int id)
    {
        var user = _mapper.Map<OutputUserDto>(await _userRepository.GetByIdAsync(id));
        if (user == null) throw new NotFoundException("Usuário não encontrado!");
        return user;
    }

    public async Task<OutputUserDto> GetByUserName(string userName)
    {
        var user = _mapper.Map<OutputUserDto>(await _userRepository.GetByUserName(userName));
        if (user == null) throw new NotFoundException("Usuário não encontrado!");
        return user;
    }

    public async Task<bool> UserExists(User user)
    {
        var searchTasks = new List<User?>
        {
            await _userRepository.GetByUserName(user.UserName!),
            await _userRepository.GetByDocument(user.Document),
            await _userRepository.GetByPhoneNumber(user.PhoneNumber!),
            await _userRepository.GetByEmail(user.Email!)
        };
        
        return searchTasks.Any(u => u != null);
    }
}