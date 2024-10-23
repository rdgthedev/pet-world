using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Commands.Cart;

namespace PetworldOficial.MVC.Controllers;

public class CartController(
    IMediator mediator) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(new CreateCartCommand(), cancellationToken);
            return View(result);
        }
        catch (Exception)
        {
            throw;
        }

        return View();
    }
}