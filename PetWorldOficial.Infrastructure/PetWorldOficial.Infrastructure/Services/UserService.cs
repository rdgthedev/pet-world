using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetWorldOficial.Application.DTOs.User.Input;
using PetWorldOficial.Application.DTOs.User.Output;
using PetWorldOficial.Application.Services.Interfaces.Identity;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Infrastructure.IdentityEntities;

namespace PetWorldOficial.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IRoleService _roleService;
    
    public UserService(
        UserManager<ApplicationUser> userManager,
        IRoleService roleService)
    {
        _userManager = userManager;
        _roleService = roleService;
    }
    
    public async Task<List<OutputUserDto>> GetAll()
    {
        return await _userManager.Users.AsNoTracking().Select(user => new OutputUserDto
        {
            Id = user.Id,
            Name = user.Name,
            UserName = user.UserName,
            Gender = user.Gender.ToString(),
            Document = user.Document,
            BirthDate = user.BirthDate,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Street = user.Street,
            Number = user.Number,
            Complement = user.Complement,
            PostalCode = user.PostalCode,
            Neighborhood = user.Neighborhood,
            City = user.City,
            State = user.State
        }).ToListAsync();
    }

    public async Task<OutputUserDto> GetById(int id)
    {
        return await _userManager.Users.AsNoTracking().Select(user => new OutputUserDto
        {
            Id = user.Id,
            Name = user.Name,
            UserName = user.UserName,
            Gender = user.Gender.ToString(),
            Document = user.Document,
            BirthDate = user.BirthDate,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Street = user.Street,
            Number = user.Number,
            Complement = user.Complement,
            PostalCode = user.PostalCode,
            Neighborhood = user.Neighborhood,
            City = user.City,
            State = user.State
        }).FirstOrDefaultAsync(user => user.Id == id);
    }
    
    public async Task<OutputUserDto> GetByUserName(string userName)
    {
        return await _userManager.Users.AsNoTracking().Select(user => new OutputUserDto
        {
            Id = user.Id,
            Name = user.Name,
            UserName = user.UserName,
            Gender = user.Gender.ToString(),
            Document = user.Document,
            BirthDate = user.BirthDate,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            Street = user.Street,
            Number = user.Number,
            Complement = user.Complement,
            PostalCode = user.PostalCode,
            Neighborhood = user.Neighborhood,
            City = user.City,
            State = user.State
        }).FirstOrDefaultAsync(user => user.UserName == userName);
    }

    public async Task<OutputUserDto> UserExists(
        string userName, 
        string? document = null, 
        string? email = null, 
        string? phoneNumber = null)
    {
        var query = _userManager.Users.AsNoTracking().AsQueryable();
        OutputUserDto? user = null;
        
        if (!string.IsNullOrEmpty(userName))
        {
            user = await query.Select(user => new OutputUserDto
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Gender = user.Gender.ToString(),
                Document = user.Document,
                BirthDate = user.BirthDate,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Street = user.Street,
                Number = user.Number,
                Complement = user.Complement,
                PostalCode = user.PostalCode,
                Neighborhood = user.Neighborhood,
                City = user.City,
                State = user.State
            }).FirstOrDefaultAsync(user => user.UserName == userName);

            if (user != null) return user;
        }
        
        if (!string.IsNullOrEmpty(document))
        {
            user = await query.Select(user => new OutputUserDto
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Gender = user.Gender.ToString(),
                Document = user.Document,
                BirthDate = user.BirthDate,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Street = user.Street,
                Number = user.Number,
                Complement = user.Complement,
                PostalCode = user.PostalCode,
                Neighborhood = user.Neighborhood,
                City = user.City,
                State = user.State
            }).FirstOrDefaultAsync(user => user.Document == document);
            
            if (user != null) return user;
        }
        
        if (!string.IsNullOrEmpty(email))
        {
            user = await query.Select(user => new OutputUserDto
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Gender = user.Gender.ToString(),
                Document = user.Document,
                BirthDate = user.BirthDate,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Street = user.Street,
                Number = user.Number,
                Complement = user.Complement,
                PostalCode = user.PostalCode,
                Neighborhood = user.Neighborhood,
                City = user.City,
                State = user.State
            }).FirstOrDefaultAsync(user => user.Email == email);
            
            if (user != null) return user;
        }
        
        if (!string.IsNullOrEmpty(phoneNumber))
        {
            user = await query.Select(user => new OutputUserDto
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName,
                Gender = user.Gender.ToString(),
                Document = user.Document,
                BirthDate = user.BirthDate,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Street = user.Street,
                Number = user.Number,
                Complement = user.Complement,
                PostalCode = user.PostalCode,
                Neighborhood = user.Neighborhood,
                City = user.City,
                State = user.State
            }).FirstOrDefaultAsync(user => user.PhoneNumber == phoneNumber);
            
            if (user != null) return user;
        }
        
        return user!;
    }

    public async Task<bool> AddToRole(UserRegisterDTO userModel, ERole typeRole)
    {
        var user = await _userManager.FindByNameAsync(userModel.UserName);

        if (user is null) throw new NotFoundException("Usuário não encontrado!");
        
        var role = await _roleService.GetByName(typeRole.ToString());

        if (role is null) throw new NotFoundException("Este perfil não existe!");

        var result = await _userManager.AddToRoleAsync(user, typeRole.ToString());

        return result.Succeeded;
    }
}