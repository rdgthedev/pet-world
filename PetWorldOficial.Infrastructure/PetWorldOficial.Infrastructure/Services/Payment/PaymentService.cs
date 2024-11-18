using System.Security.Claims;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Services;
using Stripe;
using Stripe.Checkout;
using Event = Stripe.V2.Event;

namespace PetWorldOficial.Infrastructure.Services.Payment;

public class PaymentService(
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration) : IPaymentService
{
    public async Task<(int statusCode, string sessionId)> CreateSessionCheckout(string email, List<CartItem> items,
        CancellationToken cancellationToken)
    {
        try
        {
            var domain = configuration["Domain"];
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
                SuccessUrl = domain + "/Order/Create?session_id={{CHECKOUT_SESSION_ID}}&paymentMethod=card",
                CancelUrl = domain + "/Cancel",
                CustomerEmail = email,
                PaymentMethodTypes = new List<string> { "card" }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options, cancellationToken: cancellationToken);
            httpContextAccessor.HttpContext?.Response.Headers.Append("Location", session.Url);

            return (StatusCodes.Status303SeeOther, session.Id);
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