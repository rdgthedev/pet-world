using AutoMapper;

namespace PetWorldOficial.Application.Mappers.Order;

public class CommandToDomain : Profile
{
    // public CommandToDomain()
    // {
    //     CreateMap<CreateOrderCommand, Domain.Entities.Order>()
    //         .ForCtorParam("Client", opt
    //             => opt.MapFrom(src => src.ClientId))
    //         .ForCtorParam("stripeSessionId", opt
    //             => opt.MapFrom(src => src.SessionId))
    //         .ForCtorParam("paymentMethod", opt
    //             => opt.MapFrom(src => ParsePaymentMethod(src.PaymentMethod)));
    // }
    //
    // private static EPaymentMethod ParsePaymentMethod(string paymentMethod)
    // {
    //     if (Enum.TryParse<EPaymentMethod>(paymentMethod, true, out var result))
    //         return result;
    //     
    //     throw new Exception("Método de pagamento inválido.");
    // }
}