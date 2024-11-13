using Microsoft.AspNetCore.Http;
using PetWorldOficial.Domain.Entities;
using Stripe.BillingPortal;
using Session = Stripe.Checkout.Session;

namespace PetWorldOficial.Domain.Interfaces.Services;

public interface IPaymentService
{
    Task<(int statusCode, string sessionId)> CreateSessionCheckout(string email, List<CartItem> items, CancellationToken cancellationToken);

    Task<Session?> CheckoutWebHook(
        Stream body,
        string header,
        string secret,
        CancellationToken cancellationToken);
}