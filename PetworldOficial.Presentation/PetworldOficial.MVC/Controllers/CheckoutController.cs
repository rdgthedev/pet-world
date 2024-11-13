using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Commands.Checkout;
using PetWorldOficial.Application.Commands.Order;

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
    
    [HttpGet]
    public IActionResult PaymentMethods()
    {
        return View();
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> StripeWebhook([FromBody] object stripeEvent, CancellationToken cancellationToken)
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

    [HttpPost]
    public async Task<IActionResult> Success(
        [FromQuery]string? session_id, 
        CancellationToken cancellationToken)
    {
        try
        {
            return Ok();
        }
        catch (Exception)
        {
            throw;
        }        
    }
    
    [HttpGet]
    public async Task<IActionResult> Cancel()
    {
        try
        {
            TempData["CanceledMessage"] = "Você não finalizou seu pedido, seus itens continuam no carrinho!";
            return RedirectToAction("Index","Home");
        }
        catch (Exception)
        {
            throw;
        }        
    }

    // [HttpPost("/webhook")]
    // public async Task<IActionResult> StripeWebhook()
    // {
    //     string _secretKey = "whsec_4fbcaba32bd42cf97ea98ecaff3e9677b4900056ff3ba90118e8cb776f8b48e3";
    //     var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
    //
    //     try
    //     {
    //         var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _secretKey);
    //
    //         // Handle the event
    //         // If on SDK version < 46, use class Events instead of EventTypes
    //         if (stripeEvent.Type == EventTypes.CheckoutSessionCompleted)
    //         {
    //             var session = stripeEvent.Data.Object as Session;
    //             if (session.Status == "paid")
    //             {
    //                 Console.WriteLine("BATEU AQUI");
    //             }
    //         }
    //
    //         return Ok();
    //     }
    //     catch (StripeException e)
    //     {
    //         return BadRequest();
    //     }
    // }
}