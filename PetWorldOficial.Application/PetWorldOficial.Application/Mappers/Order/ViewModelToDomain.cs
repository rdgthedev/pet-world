using AutoMapper;
using PetWorldOficial.Application.ViewModels.Order;
using PetWorldOficial.Domain.Enums;
using Stripe;

namespace PetWorldOficial.Application.Mappers.Order;

public class ViewModelToDomain : Profile
{
    public ViewModelToDomain()
    {
        CreateMap<OrderDetailsViewModel, Domain.Entities.Order>()
            .ConstructUsing(o => new Domain.Entities.Order(
                o.ClientId,
                o.StripeSessionId,
                ParsePaymentMethod(o.PaymentMethod)))
            .AfterMap((src, dest) =>
            {
                dest.AddItems(src.Items.ToArray());
                dest.CalculateTotalPrice();
            });
    }

    private static EPaymentMethod ParsePaymentMethod(string paymentMethod)
    {
        if (Enum.TryParse<EPaymentMethod>(paymentMethod, true, out var result))
            return result;

        throw new Exception("Método de pagamento inválido.");
    }
}