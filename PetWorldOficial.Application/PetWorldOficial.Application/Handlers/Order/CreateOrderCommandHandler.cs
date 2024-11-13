using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Application.Commands.Order;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Interfaces.Services;

namespace PetWorldOficial.Application.Handlers.Order;

public class CreateOrderCommandHandler(
    IPaymentService paymentService,
    IHttpContextAccessor httpContextAccessor,
    IUserService userService,
    ICartService cartService,
    IMapper mapper) : IRequestHandler<CreateOrderCommand, (bool success, string message)>
{
    private const string _secretKey = "whsec_4fbcaba32bd42cf97ea98ecaff3e9677b4900056ff3ba90118e8cb776f8b48e3";

    public async Task<(bool success, string message)> Handle(CreateOrderCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var session = await paymentService.CheckoutWebHook(
                httpContextAccessor.HttpContext.Request.Body,
                httpContextAccessor.HttpContext.Request.Headers["Stripe-Signature"],
                _secretKey,
                cancellationToken);

            if (session is null)
                throw new Exception("Ocorreu um erro!");

            var email = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)
                ?.Value;
            var userId = (await userService.GetByEmailAsync(email, cancellationToken))?.Id ?? 0;
            var cart = await cartService.GetOrCreateCartForUserAsync(userId, cancellationToken);

            var order = new Domain.Entities.Order(userId);
            order.AddItems(cart.Items!.ToArray());

            await cartService.DeleteAsync(mapper.Map<DeleteCartCommand>(cart), cancellationToken);
            return (success: true, message: "Pagamento realizado com sucesso!");
        }
        catch (Exception)
        {
            throw;
        }
    }
}