using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Commands.Checkout;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Interfaces.Services;

namespace PetWorldOficial.Application.Handlers.Checkout;

public class CreateCheckoutSessionCommandHandler(
    IPaymentService paymentService,
    IUserService userService,
    ICartService cartService,
    IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateCheckoutSessionCommand, StatusCodeResult>
{
    public async Task<StatusCodeResult> Handle(CreateCheckoutSessionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var email = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)
                ?.Value;
            var userId = (await userService.GetByEmailAsync(email, cancellationToken))?.Id ?? 0;
            var cart = await cartService.GetOrCreateCartForUserAsync(userId, cancellationToken);

            var statusoCode = await paymentService.CreateSessionCheckout(email, cart.Items, cancellationToken);

            return new StatusCodeResult(statusoCode);
        }
        catch (Exception)
        {
            throw;
        }
    }
}