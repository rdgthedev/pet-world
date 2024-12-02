using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Domain.Exceptions;

namespace PetworldOficial.MVC.Controllers;

public class CartController(IMediator mediator) : Controller
{
    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(new GetOrCreateCartCommand(), cancellationToken);
            return View(result);
        }
        catch (UserNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Login", "Auth");
        }
        catch (QuantityOfProductOutOfStockException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View();
        }
        catch (Exception)
        {
            throw;
        }

        return View();
    }
    
    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> AddItem(
        AddOrIncreaseItemToCartCommand command,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return Json(new { message = "Produto não encontrado!" });
        }

        try
        {
            var result = await mediator.Send(command, cancellationToken);
            return Json(new { result.success, result.subTotalPrice, result.totalPrice});
        }
        catch (QuantityOfProductOutOfStockException e)
        {
            return Json(new { success = false, message = e.Message });
        }
        catch (ProductNotFoundException e)
        {
            return Json(new { success = false, message = e.Message });
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "Ocorreu um erro interno!" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> DecreaseItem(
        DecreaseItemCommand command,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Produto não encontrado!";
            return RedirectToAction("Index", "Product");
        }

        try
        {
            var result = await mediator.Send(command, cancellationToken);
            return Json(new { result.success, result.subTotalPrice, result.totalPrice});
        }
        catch (ProductNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Index", "Product");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno";
            return RedirectToAction("Index", "Product");
        }
    }
}