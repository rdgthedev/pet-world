using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Commands.CartItem;

namespace PetworldOficial.MVC.Controllers;

public class CartItemController(IMediator mediator) : Controller
{
    [HttpPost]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(new DeleteItemCommand { Id = id }, cancellationToken);
            return Ok(new{result.success, result.totalPrice});
        }
        catch (Exception)
        {
            throw;
        }
    }
}