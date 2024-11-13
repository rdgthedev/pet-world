using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PetWorldOficial.Application.Commands.Product;
using PetWorldOficial.Application.Queries.Product;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Application.ViewModels.Product;
using PetWorldOficial.Application.ViewModels.Supplier;
using PetWorldOficial.Domain.Exceptions;

namespace PetworldOficial.MVC.Controllers;

public class ProductController(
    IMediator mediator,
    IWebHostEnvironment webHostEnvironment,
    ICategoryService categoryService,
    ISupplierService supplierService,
    IMemoryCache memoryCache) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var products = Enumerable.Empty<ProductDetailsViewModel>();

        try
        {
            products = await mediator.Send(new GetAllProductsQuery(), cancellationToken);
            return View(products);
        }
        catch (ProductNotFoundException e)
        {
            TempData["ProductNotFound"] = e.Message;
            return View(products);
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToPage("Error");
        }
    }

    //
    // [HttpGet]
    // public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    // {
    //     try
    //     {
    //         var product = await productService.GetById(id, cancellationToken);
    //         return View();
    //     }
    //     catch (NotFoundException ex)
    //     {
    //         TempData["ErrorMessage"] = ex.Message;
    //         return View();
    //     }
    //     catch (Exception)
    //     {
    //         TempData["ErrorMessage"] = "Ocorreu um erro interno!";
    //         return View();
    //     }
    // }
    //
    [HttpGet()]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new CreateProductCommand(), cancellationToken);
        return View(result);
    }


    [HttpPost]
    public async Task<IActionResult> Create(
        [FromForm] CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            if (memoryCache.TryGetValue("ProductCategories", out IEnumerable<CategoryDetailsViewModel>? categories))
                command.Categories = categories;

            if (memoryCache.TryGetValue("Suppliers", out IEnumerable<SupplierDetailsViewModel>? suppliers))
                command.Suppliers = suppliers;

            return View(command);
        }

        try
        {
            command.BaseUrl = webHostEnvironment.ContentRootPath;
            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index");
        }
        catch (ProductAlreadyExistsException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View(command);
        }
        catch (InvalidExtensionException e)
        {
            TempData["ErrorMessage"] = e.Message;
            ModelState.Clear();
            return View(command);
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return View(command);
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(
        [FromRoute] int id,
        UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            command.Id = id;

            var result = await mediator.Send(command, cancellationToken);
            ModelState.Clear();
            return View(result);
        }
        catch (NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View();
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Update(
        [FromForm] UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            command.Categories = await categoryService.GetAllProductCategories(cancellationToken);
            command.Suppliers = await supplierService.GetAllAsync(cancellationToken);
            
            return View(command);
        }

        try
        {
            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.Message;
            return RedirectToAction("Index");
        }
        catch (NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Index", "Product");
        }
        catch (InvalidExtensionException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Index", "Product");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Index", "Product");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(new DeleteProductCommand(id), cancellationToken);
            return View(result);
        }
        catch (NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("Index", "Product");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Index", "Product");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] DeleteProductCommand command,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View();

        try
        {
            var result = await mediator.Send(command, cancellationToken);
            TempData["SuccessMessage"] = result.Message;

            return RedirectToAction("Index", "Product");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Index", "Product");
        }
    }
}