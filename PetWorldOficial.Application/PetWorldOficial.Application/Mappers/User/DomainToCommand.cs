using AutoMapper;
using PetWorldOficial.Application.Commands.User;

namespace PetWorldOficial.Application.Mappers.User;

public class DomainToCommand : Profile
{
    public DomainToCommand()
    {
        CreateMap<Domain.Entities.User, UpdateUserCommand>()
            .ConstructUsing(user
                => new UpdateUserCommand
                {
                    Id = user.Id,
                    // UserName = user.UserName!,
                    Gender = user.Gender.ToString(),
                    BirthDate = user.BirthDate,
                    Document = user.Document,
                    Email = user.Email!,
                    PhoneNumber = user.PhoneNumber!,
                    Street = user.Street,
                    Number = user.Number!,
                    PostalCode = user.PostalCode,
                    Neighborhood = user.Neighborhood,
                    Complement = user.Complement!,
                    City = user.City,
                    State = user.State
                });
    }
}