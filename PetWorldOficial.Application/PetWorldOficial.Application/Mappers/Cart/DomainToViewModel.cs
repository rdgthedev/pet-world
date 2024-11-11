using AutoMapper;
using PetWorldOficial.Application.ViewModels.Cart;

namespace PetWorldOficial.Application.Mappers.Cart;

public class DomainToViewModel : Profile
{
    public DomainToViewModel()
    {
        CreateMap<Domain.Entities.Cart, CartDetailsViewModel>()
            .ConstructUsing(c => new CartDetailsViewModel
            {
                Id = c.Id,
                ClientId = c.ClientId,
                ExpiresDate = c.ExpiresDate,
                Items = c.Items,
                TotalPrice = c.Items.Sum(i => i.TotalPrice)
            });
    }
}