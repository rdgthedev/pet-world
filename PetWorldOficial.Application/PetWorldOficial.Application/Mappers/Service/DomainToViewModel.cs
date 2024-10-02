using AutoMapper;
using PetWorldOficial.Application.ViewModels.Service;

namespace PetWorldOficial.Application.Mappers.Service
{
    public class DomainToViewModel : Profile
    {
        public DomainToViewModel()
        {
            CreateMap<Domain.Entities.Service, ServiceDetailsViewModel>();
            // .ConstructUsing(s => new ServiceDetailsViewModel
            // {
            //     Id = s.Id,
            //     Name = s.Name,
            //     CategoryTitle = s.Category.Title,
            //     DurationInMinutes = s.DurationInMinutes,
            //     ImageUrl = s.ImageUrl,
            //     Price = s.Price
            // });
        }
    }
}