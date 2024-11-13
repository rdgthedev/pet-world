using System.Security.Claims;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Services;
using Stripe;
using Stripe.Checkout;
using Event = Stripe.V2.Event;

namespace PetWorldOficial.Infrastructure.Services.Payment;

public class PaymentService(IHttpContextAccessor httpContextAccessor) : IPaymentService
{
    public async Task<int> CreateSessionCheckout(string email, List<CartItem> items,
        CancellationToken cancellationToken)
    {
        try
        {
            var domain = "https://localhost:44383";
            var options = new SessionCreateOptions
            {
                LineItems = items.Select(ci => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(ci.Product.Price * 100),
                        Currency = "brl",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = ci.Product.Name
                        }
                    },
                    Quantity = ci.Quantity,
                }).ToList(),
                Mode = "payment",
                SuccessUrl = domain + "/Home",
                CancelUrl = domain + "/",
                CustomerEmail = email,
                PaymentMethodTypes = new List<string> { "card" }
            };
            var service = new SessionService();
            var session = service.Create(options);

            httpContextAccessor.HttpContext.Response.Headers.Append("Location", session.Url);
            return await Task.FromResult(StatusCodes.Status303SeeOther);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Session?> CheckoutWebHook(
        Stream body,
        string header,
        string secret,
        CancellationToken cancellationToken)
    {
        try
        {
            using var reader = new StreamReader(body).ReadToEndAsync(cancellationToken);
            var json = await reader;

            var stripeEvent = EventUtility.ConstructEvent(
                json,
                header,
                secret);

            if (stripeEvent.Type == "checkout.session.completed")
                return stripeEvent.Data.Object as Session;

            return null!;
        }
        catch (Exception)
        {
            throw;
        }
    }
}