using AutoMapper;
using PetWorldOficial.Application.ViewModels.User;

namespace PetWorldOficial.Application.Mappers.User;

public class ViewModelToUserEntityProfiles : Profile
{
    public ViewModelToUserEntityProfiles()
    {
        CreateMap<RegisterUserViewModel, Domain.Entities.User>()
            .ConstructUsing(dto
                => new Domain.Entities.User(
                    dto.Name,
                    dto.UserName,
                    dto.Gender,
                    (DateTime)dto.BirthDate!,
                    dto.Document,
                    dto.Email,
                    dto.PhoneNumber,
                    dto.Street,
                    (int)dto.Number!,
                    dto.PostalCode,
                    dto.Neighborhood,
                    dto.Complement,
                    dto.City,
                    dto.State));

        CreateMap<UserDetailsViewModel, Domain.Entities.User>()
            .ConstructUsing(dto
                => new Domain.Entities.User(
                    dto.Name,
                    dto.UserName,
                    dto.Gender,
                    (DateTime)dto.BirthDate!,
                    dto.Document,
                    dto.Email,
                    dto.PhoneNumber,
                    dto.Street,
                    (int)dto.Number!,
                    dto.PostalCode,
                    dto.Neighborhood,
                    dto.Complement,
                    dto.City,
                    dto.State));

        // CreateMap<UpdateUserViewModel, Domain.Entities.User>()
        //     .ConstructUsing(dto
        //         => new Domain.Entities.User(
        //             dto.Name,
        //             dto.UserName,
        //             dto.Gender,
        //             (DateTime)dto.BirthDate!,
        //             dto.Document,
        //             dto.Email,
        //             dto.PhoneNumber,
        //             dto.Street,
        //             dto.Number,
        //             dto.PostalCode,
        //             dto.Neighborhood,
        //             dto.Complement,
        //             dto.City,
        //             dto.State));

        CreateMap<UpdateUserViewModel, Domain.Entities.User>()
            .ForMember(dest => dest.UserName, opt => opt.Ignore())
            .ForMember(dest => dest.SecurityStamp, opt => opt.Ignore())
            .ForMember(dest => dest.EmailConfirmed, opt => opt.Ignore())
            .ForMember(dest => dest.LockoutEnabled, opt => opt.Ignore())
            .ForMember(dest => dest.LockoutEnd, opt => opt.Ignore())
            .ForMember(dest => dest.PhoneNumberConfirmed, opt => opt.Ignore())
            .ForMember(dest => dest.TwoFactorEnabled, opt => opt.Ignore())
            .ForMember(dest => dest.AccessFailedCount, opt => opt.Ignore())
            .ForMember(dest => dest.NormalizedUserName, opt => opt.Ignore())
            .ForMember(dest => dest.NormalizedEmail, opt => opt.Ignore())
            .ForMember(dest => dest.ConcurrencyStamp, opt => opt.Ignore())
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Document, opt => opt.MapFrom(src => src.Document))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.Complement, opt => opt.MapFrom(src => src.Complement))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForMember(dest => dest.Neighborhood, opt => opt.MapFrom(src => src.Neighborhood))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City))
            .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State));
    }
}