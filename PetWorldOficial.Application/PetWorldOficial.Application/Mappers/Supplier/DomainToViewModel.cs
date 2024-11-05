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
                Email = s.Email,
                CNPJ = s.CNPJ,
                CellPhone = s.CellPhone,
                Street = s.Street,
                Number = s.Number,
                Neighborhood = s.Neighborhood,
                Complement = s.Complement,
                PostalCode = s.PostalCode,
                City = s.City,
                State = s.State,
                Status = s.Status.ToString()
            });
    }
}