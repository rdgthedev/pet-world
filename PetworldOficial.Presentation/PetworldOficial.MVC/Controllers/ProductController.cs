using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PetWorldOficial.Application.Commands.Product;
using PetWorldOficial.Application.Queries.Product;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Application.ViewModels.Product;
using PetWorldOficial.Application.ViewModels.Supplier;
using PetWorldOficial.Domain.Exceptions;

namespace PetworldOficial.MVC.Controllers;

public class ProductController(
    IMediator mediator,
    IWebHostEnvironment webHostEnvironment,
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
    [HttpGet]
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
            if (memoryCache.TryGetValue("Categories", out IEnumerable<CategoryDetailsViewModel>? categories))
                command.Categories = categories;

            if (memoryCache.TryGetValue("Suppliers", out IEnumerable<SupplierDetailsViewModel>? suppliers))
                command.Suppliers = suppliers;

            return View(command);
        }

        try
        {
            command.BaseUrl = webHostEnvironment.WebRootPath;
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
    //
    // [HttpGet]
    // [Authorize(Roles = "Admin")]
    // public async Task<IActionResult> Update([FromRoute] int id, CancellationToken cancellationToken)
    // {
    //     try
    //     {
    //         var suppliers = await supplierRepository.GetAllAsync(cancellationToken);
    //         var productResult = await productService.GetById(id, cancellationToken);
    //
    //         return View(new UpdateProductViewModel
    //         {
    //             Id = productResult.Id,
    //             Name = productResult.Name,
    //             Description = productResult.Description,
    //             ImageUrl = productResult.ImageUrl,
    //             Price = productResult.Price,
    //             SupplierId = productResult.SupplierId,
    //             Suppliers = suppliers
    //         });
    //     }
    //     catch (NotFoundException e)
    //     {
    //         TempData["ErrorMessage"] = e.Message;
    //         return View();
    //     }
    //     catch (Exception)
    //     {
    //         TempData["ErrorMessage"] = "Ocorreu um erro interno!";
    //         return View();
    //     }
    // }
    //
    // [HttpPost]
    // public async Task<IActionResult> Update(
    //     [FromForm] UpdateProductViewModel updateProductDto,
    //     IFormFile? file,
    //     CancellationToken cancellationToken)
    // {
    //     if (!ModelState.IsValid)
    //         return View(updateProductDto);
    //
    //     try
    //     {
    //         updateProductDto.Suppliers = await supplierRepository.GetAllAsync(cancellationToken);
    //         var productResult = await productService.GetById(updateProductDto.Id, cancellationToken);
    //
    //         switch (file)
    //         {
    //             case not null:
    //                 updateProductDto.ImageUrl = imageService.GenerateImageName(file, webHostEnvironment.WebRootPath);
    //                 await imageService.SaveImage(file, webHostEnvironment.WebRootPath, updateProductDto.ImageUrl);
    //                 break;
    //             default:
    //                 updateProductDto.ImageUrl = productResult.ImageUrl;
    //                 break;
    //         }
    //
    //         await productService.Update(new UpdateProductDTO(
    //                 updateProductDto.Id,
    //                 updateProductDto.Name,
    //                 updateProductDto.Description,
    //                 updateProductDto.ImageUrl,
    //                 updateProductDto.Price,
    //                 updateProductDto.SupplierId),
    //             cancellationToken);
    //
    //         TempData["SuccessMessage"] = "Produto alterado com sucesso!";
    //         return View(updateProductDto);
    //     }
    //     catch (NotFoundException e)
    //     {
    //         TempData["ErrorMessage"] = e.Message;
    //         return RedirectToAction("Index", "Product");
    //     }
    //     catch (InvalidExtensionException e)
    //     {
    //         TempData["ErrorMessage"] = e.Message;
    //         return RedirectToAction("Index", "Product");
    //     }
    //     catch (Exception)
    //     {
    //         TempData["ErrorMessage"] = "Ocorreu um erro interno!";
    //         return RedirectToAction("Error", "Home");
    //     }
    // }
    //
    // [HttpGet]
    // public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    // {
    //     try
    //     {
    //         var product = mapper.Map<DeleteProductDTO>(await productService.GetById(id, cancellationToken));
    //
    //         var supplier = await supplierRepository.GetByIdAsync(product.SupplierId, cancellationToken);
    //
    //         if (supplier is null) throw new NotFoundException("Fornecedor não encontrado!");
    //
    //         product.Supplier = supplier;
    //
    //         return View(product);
    //     }
    //     catch (NotFoundException e)
    //     {
    //         TempData["ErrorMessage"] = e.Message;
    //         return RedirectToAction("Index", "Product");
    //     }
    //     catch (Exception)
    //     {
    //         TempData["ErrorMessage"] = "Ocorreu um erro interno!";
    //         return RedirectToAction("Index", "Product");
    //     }
    // }
    //
    // [HttpPost]
    // public async Task<IActionResult> Delete([FromForm] DeleteProductDTO deleteProductDto, CancellationToken cancellationToken)
    // {
    //     if (!ModelState.IsValid) return View();
    //
    //     try
    //     {
    //         await productService.Delete(deleteProductDto, cancellationToken);
    //         TempData["SuccessMessage"] = "Produto deletado com sucesso!";
    //
    //         return RedirectToAction("Index", "Product");
    //     }
    //     catch (Exception)
    //     {
    //         TempData["ErrorMessage"] = "Ocorreu um erro interno!";
    //         return RedirectToAction("Index", "Product");
    //     }
    // }
}