using AutoMapper;
using PetWorldOficial.Application.ViewModels.User;

namespace PetWorldOficial.Application.Mappers.User;

public class UserEntityToViewModelsProfiles : Profile
{
    public UserEntityToViewModelsProfiles()
    {
        CreateMap<Domain.Entities.User, UserDetailsViewModel>()
            .ConstructUsing(user
                => new UserDetailsViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    UserName = user.UserName!,
                    PasswordHash = user.PasswordHash!,
                    SecurityStamp = user.SecurityStamp!,
                    Gender = user.Gender,
                    BirthDate = user.BirthDate,
                    Document = user.Document,
                    Email = user.Email!,
                    PhoneNumber = user.PhoneNumber!,
                    Street = user.Street,
                    Number = user.Number!,
                    PostalCode = user.PostalCode,
                    Neighborhood = user.Neighborhood,
                    Complement = user.Complement,
                    City = user.City,
                    State = user.State
                });
        
        CreateMap<Domain.Entities.User, UpdateUserViewModel>()
            .ConstructUsing(user
                => new UpdateUserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    UserName = user.UserName!,
                    Gender = user.Gender,
                    BirthDate = user.BirthDate,
                    Document = user.Document,
                    Email = user.Email!,
                    PhoneNumber = user.PhoneNumber!,
                    Street = user.Street,
                    Number = user.Number!,
                    PostalCode = user.PostalCode,
                    Neighborhood = user.Neighborhood,
                    Complement = user.Complement,
                    City = user.City,
                    State = user.State
                });
    }
}