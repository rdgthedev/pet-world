using AutoMapper;
using PetWorldOficial.Application.ViewModels.Supplier;

namespace PetWorldOficial.Application.Mappers.Supplier;

public class DomainToViewModel : Profile
{
    public DomainToViewModel()
    {
        CreateMap<Domain.Entities.Supplier, SupplierDetailsViewModel>()
            .ConstructUsing(s => new SupplierDetailsViewModel
            {
                Id = s.Id,
                Name = s.Name,
                CellPhone = s.CellPhone,
                Status = s.Status.ToString()
            });
    }
}