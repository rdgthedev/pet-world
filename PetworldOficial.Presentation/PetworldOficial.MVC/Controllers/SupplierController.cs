using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Commands.Supplier;
using PetWorldOficial.Application.Queries.Supplier;
using PetWorldOficial.Domain.Exceptions;

namespace PetworldOficial.MVC.Controllers;

public class SupplierController(
    IMediator mediator) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(new GetAllSuppliersQuery(), cancellationToken);
            return View(result);
        }
        catch (SupplierNotFoundException e)
        {
            TempData["NotFoundSupplier"] = e.Message;
            return View();
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return View();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Register()
        => View();

    [HttpPost]
    public async Task<IActionResult> Register(
        RegisterSupplierCommand command,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(command);
        
        try
        {
            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index");
        }
        catch (SupplierAlreadyExistsException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            TempData["NotFoundSupplier"] = "Nenhum ";
            TempData["ErrorMessage"] = "Ocorreu um erro interno";
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(new UpdateSupplierCommand { Id = id }, cancellationToken);
            return View(result);
        }
        catch (SupplierNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update(
        [FromForm] UpdateSupplierCommand command,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(command);

        try
        {
            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(new DeleteSupplierCommand { Id = id }, cancellationToken);
            return View(result);
        }
        catch (SupplierNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete(
        [FromForm] DeleteSupplierCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Index");
        }
    }
}