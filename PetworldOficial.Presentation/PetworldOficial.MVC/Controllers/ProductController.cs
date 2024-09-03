using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.DTOs.Product;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Product;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetworldOficial.MVC.Controllers;

public class ProductController(
    IProductService productService,
    ISupplierRepository supplierRepository,
    IWebHostEnvironment webHostEnvironment,
    IImageService imageService,
    IMapper mapper)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        try
        {
            var products = await productService.GetAll(cancellationToken);
            return View(products);
        }
        catch (NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View();
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToPage("Error");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var product = await productService.GetById(id, cancellationToken);
            return View();
        }
        catch (NotFoundException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View();
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return View();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
        => View(new RegisterProductDTO { Suppliers = await supplierRepository.GetAllAsync(cancellationToken) });

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromForm] RegisterProductDTO registerProductDto,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        registerProductDto.Suppliers = await supplierRepository.GetAllAsync(cancellationToken);

        if (!ModelState.IsValid)
        {
            if (file == null!)
                ModelState.AddModelError(string.Empty, "A imagem é obrigatória!");

            return View(registerProductDto);
        }

        try
        {
            var supplier = await supplierRepository.GetByIdAsync(registerProductDto.SupplierId, cancellationToken);

            if (supplier == null) throw new NotFoundException("Fornecedor não encontrado!");

            registerProductDto.ImageUrl = imageService.GenerateImageName(file, webHostEnvironment.WebRootPath);

            await productService.Create(registerProductDto, cancellationToken);
            await imageService.SaveImage(file, webHostEnvironment.WebRootPath, registerProductDto.ImageUrl);

            TempData["SuccessMessage"] = "Produto criado com sucesso!";
            ModelState.Clear();
            return View(registerProductDto);
        }
        catch (NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            ModelState.Clear();
            return View(registerProductDto);
        }
        catch (InvalidExtensionException e)
        {
            TempData["ErrorMessage"] = e.Message;
            ModelState.Clear();
            return View(registerProductDto);
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            ModelState.Clear();
            return View(registerProductDto);
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var suppliers = await supplierRepository.GetAllAsync(cancellationToken);
            var productResult = await productService.GetById(id, cancellationToken);

            return View(new UpdateProductViewModel
            {
                Id = productResult.Id,
                Name = productResult.Name,
                Description = productResult.Description,
                ImageUrl = productResult.ImageUrl,
                Price = productResult.Price,
                SupplierId = productResult.SupplierId,
                Suppliers = suppliers
            });
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
        [FromForm] UpdateProductViewModel updateProductDto,
        IFormFile? file,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(updateProductDto);

        try
        {
            updateProductDto.Suppliers = await supplierRepository.GetAllAsync(cancellationToken);
            var productResult = await productService.GetById(updateProductDto.Id, cancellationToken);

            switch (file)
            {
                case not null:
                    updateProductDto.ImageUrl = imageService.GenerateImageName(file, webHostEnvironment.WebRootPath);
                    await imageService.SaveImage(file, webHostEnvironment.WebRootPath, updateProductDto.ImageUrl);
                    break;
                default:
                    updateProductDto.ImageUrl = productResult.ImageUrl;
                    break;
            }

            await productService.Update(new UpdateProductDTO(
                    updateProductDto.Id,
                    updateProductDto.Name,
                    updateProductDto.Description,
                    updateProductDto.ImageUrl,
                    updateProductDto.Price,
                    updateProductDto.SupplierId),
                cancellationToken);

            TempData["SuccessMessage"] = "Produto alterado com sucesso!";
            return View(updateProductDto);
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
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var product = mapper.Map<DeleteProductDTO>(await productService.GetById(id, cancellationToken));

            var supplier = await supplierRepository.GetByIdAsync(product.SupplierId, cancellationToken);

            if (supplier is null) throw new NotFoundException("Fornecedor não encontrado!");

            product.Supplier = supplier;

            return View(product);
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
    public async Task<IActionResult> Delete([FromForm] DeleteProductDTO deleteProductDto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid) return View();

        try
        {
            await productService.Delete(deleteProductDto, cancellationToken);
            TempData["SuccessMessage"] = "Produto deletado com sucesso!";

            return RedirectToAction("Index", "Product");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Index", "Product");
        }
    }
}