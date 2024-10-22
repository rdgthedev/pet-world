using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PetworldOficial.MVC.Controllers;

public class CartController(
	IMediator mediator) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(int? productId, CancellationToken cancellationToken)
    {
		try
		{

		}
		catch (Exception)
		{
			throw;
		}

		return View();
    }
}