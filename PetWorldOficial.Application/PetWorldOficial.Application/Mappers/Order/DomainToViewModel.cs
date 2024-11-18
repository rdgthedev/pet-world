using AutoMapper;
using PetWorldOficial.Application.ViewModels.Order;

namespace PetWorldOficial.Application.Mappers.Order;

public class DomainToViewModel : Profile
{
    public DomainToViewModel()
    {
        CreateMap<Domain.Entities.Order, OrderDetailsViewModel>()
            .ConstructUsing(o => new OrderDetailsViewModel
            {
                Id = o.Id,
                ClientId = o.ClientId,
                PaymentMethod = o.PaymentMethod.ToString(),
                Client = o.Client,
                Code = o.Code,
                Items = o.Items,
                CreatedAt = o.CreatedAt,
                PaymentDate = o.PaymentDate,
                Status= o.Status.ToString(),
                StripeSessionId = o.StripeSessionId,
                TotalPrice = o.TotalPrice
            });
    }
}