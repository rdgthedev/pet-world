using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Commands.Cart;
using PetWorldOficial.Domain.Exceptions;

namespace PetworldOficial.MVC.Controllers;

public class CartController(IMediator mediator) : Controller
{
    [HttpGet]
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
        catch (Exception)
        {
            throw;
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddItem(
        AddOrIncreaseItemToCartCommand command,
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
            return Json(new { result.success, result.totalPrice });
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
            return Json(new { result.success, result.totalPrice });
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