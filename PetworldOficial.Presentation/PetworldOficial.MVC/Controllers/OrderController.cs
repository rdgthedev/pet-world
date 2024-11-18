using MediatR;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.Commands.Order;
using PetWorldOficial.Application.Queries.Order;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetworldOficial.MVC.Controllers;

public class OrderController(IMediator mediator, IOrderService orderService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        try
        {
            var orders = await mediator.Send(new GetAllOrdersQuery(), cancellationToken);
            return View(orders);
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(
        [FromRoute] GetOrderByIdCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(command, cancellationToken);
            return View(result);
        }
        catch (OrderNotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create(
        [FromQuery] string session_id,
        [FromQuery] string paymentMethod,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new CreateOrderCommand { SessionId = session_id, PaymentMethod = paymentMethod };
            var result = await mediator.Send(command, cancellationToken);
            TempData["OrderCreated"] = result.message;
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet]
    public async Task<IActionResult> AwaitingPickUp(
        [FromRoute] UpdateStatusToAwaitingPickUpCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.message;
        }
        catch (UnableToChangeOrderStatusException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> InTransit(
        [FromRoute] UpdateStatusToInTransitCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.message;
        }
        catch (UnableToChangeOrderStatusException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Delivered(
        [FromRoute] UpdateStatusToDeliveredCommand command, 
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.message;
        }
        catch (UnableToChangeOrderStatusException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public async Task<IActionResult> Cancel(
        [FromRoute] UpdateStatusToCanceledCommand command, 
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.message;
        }
        catch (UnableToChangeOrderStatusException e)
        {
            TempData["ErrorMessage"] = e.Message;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
        }

        return RedirectToAction("Index");
    }
}