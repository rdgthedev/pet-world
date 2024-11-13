using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Commands.Order;

namespace PetworldOficial.MVC.Controllers;

public class OrderController(IMediator mediator) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Create(
        [FromQuery] string session_id,
        [FromQuery] string paymentMethod,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(new CreateOrderCommand { SessionId = session_id, PaymentMethod = paymentMethod },cancellationToken);
            TempData["OrderCreated"] = result.message;
            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            throw;
        }
    }
}