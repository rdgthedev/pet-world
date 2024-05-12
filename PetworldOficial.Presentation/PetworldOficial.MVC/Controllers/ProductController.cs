using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.DTOs.Product.Input;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.ApplicationServices;
using PetWorldOficial.Domain.Interfaces.Repositories;
using PetworldOficial.MVC.ViewModels.Product;

namespace PetworldOficial.MVC.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IImageService _imageService;
    
    public ProductController(
        IProductRepository productRepository,
        ISupplierRepository supplierRepository,
        IWebHostEnvironment webHostEnvironment,
        IImageService imageService)
    {
        _productRepository = productRepository;
        _supplierRepository = supplierRepository;
        _webHostEnvironment = webHostEnvironment;
        _imageService = imageService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Products()
    {
        try
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToPage("Error");
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Product([FromRoute] int id)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null) throw new NotFoundException("Produto não encontrado");

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
    public async Task<IActionResult> Create()
        => View(new CreateViewModel(null, await _supplierRepository.GetAllAsync()));
    
    
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateViewModel productModel)
    {
        if (!ModelState.IsValid) 
            return View(new CreateViewModel(productModel.Product, await _supplierRepository.GetAllAsync()));

        try
        {
            var supplier = await _supplierRepository.GetByIdAsync(productModel.Product.SupplierId);

            if (supplier == null) throw new NotFoundException("Fornecedor não encontrado!");

            var imageUrl = await _imageService.ProcessImage(productModel.Product.Image, _webHostEnvironment.WebRootPath);
            
            var product = new Product(
                productModel.Product.Name,
                productModel.Product.Description,
                productModel.Product.Price,
                imageUrl,
                supplier!.Id);

            await _productRepository.CreateAsync(product);
            TempData["SuccessMessage"] = "Produto criado com sucesso!";
            
            ModelState.Clear();
            return View(new CreateViewModel(null!, await _supplierRepository.GetAllAsync()));
        }
        catch (NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            ModelState.Clear();
            return View(new CreateViewModel(null!, await _supplierRepository.GetAllAsync()));
        }
        catch(Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            ModelState.Clear();
            return View(new CreateViewModel(null!, await _supplierRepository.GetAllAsync()));
        }
    }
    
    // [HttpPost]
    // public async Task<IActionResult> Update([FromForm] ProductUpdateDTO productUpdateDto)
    // {
    //     if (!ModelState.IsValid) return View(productUpdateDto);
    //
    //     try
    //     {
    //         var product = await _productRepository.GetByIdAsync(productUpdateDto.Id);
    //
    //         if (product is null) throw new NotFoundException("Produto não encontrado!");
    //
    //         var imageUrl = _processImageService.ProcessImage(productUpdateDto.Image!, _webHostEnvironment.WebRootPath);
    //         
    //         product.Update(productUpdateDto);
    //
    //         await _productRepository.Update(product);
    //         TempData["SuccessMessage"] = "Produto alterado com sucesso!";
    //         
    //         return View();
    //     }
    //     catch (NotFoundException e)
    //     {
    //         ModelState.AddModelError(String.Empty, e.Message);
    //         return View();
    //     }
    //     catch (Exception e)
    //     {
    //         TempData["ErrorMessage"] = "Ocorreu um erro interno!";
    //         return View();
    //     }
    // }
    
    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] int id)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null) throw new NotFoundException("Produto não encontrado!");

            await _productRepository.Delete(product);
            TempData["SuccessMessage"] = "Produto deletado com sucesso!";

            return View();
        }
        catch (NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return View();
        }
        catch (Exception e)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return View();
        }
    }
}