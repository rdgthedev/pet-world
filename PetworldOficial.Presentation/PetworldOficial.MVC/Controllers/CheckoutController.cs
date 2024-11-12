using System.Diagnostics.Tracing;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Commands.Checkout;
using PetWorldOficial.Application.Commands.Order;
using PetWorldOficial.Application.Services.Interfaces;
using Stripe;
using Stripe.Checkout;

namespace PetworldOficial.MVC.Controllers;

public class CheckoutController(IMediator mediator) : Controller
{
    public async Task<IActionResult> Checkout(CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(new CreateCheckoutSessionCommand(), cancellationToken);
            return result;
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook(CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(new CreateOrderCommand(), cancellationToken);
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Index", "Home");
        }

        return Ok();
    }
}