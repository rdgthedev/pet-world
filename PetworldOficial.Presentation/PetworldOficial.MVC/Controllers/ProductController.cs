using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.DTOs.Product;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.ApplicationServices;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetworldOficial.MVC.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;
    
    public ProductController(
        IProductService productService,
        ISupplierRepository supplierRepository,
        IWebHostEnvironment webHostEnvironment,
        IImageService imageService,
        IMapper mapper)
    {
        _productService = productService;
        _supplierRepository = supplierRepository;
        _webHostEnvironment = webHostEnvironment;
        _imageService = imageService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var products = await _productService.GetAll();
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
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        try
        {
            var product = await _productService.GetById(id);
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
        => View(new RegisterProductDTO { Suppliers = await _supplierRepository.GetAllAsync() });
    
    
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] RegisterProductDTO registerProductDto, IFormFile file)
    {
        registerProductDto.Suppliers = await _supplierRepository.GetAllAsync();

        if (!ModelState.IsValid)
        {
            if(file == null)
                ModelState.AddModelError(string.Empty, "A imagem é obrigatória!");
            
            return View(registerProductDto);
        }
        
        try
        {
            var supplier = await _supplierRepository.GetByIdAsync(registerProductDto.SupplierId);

            if (supplier == null) throw new NotFoundException("Fornecedor não encontrado!");
            
            registerProductDto.ImageUrl = _imageService.GenerateImageName(file, _webHostEnvironment.WebRootPath);
            
            await _productService.Create(registerProductDto);
            await _imageService.SaveImage(file, _webHostEnvironment.WebRootPath, registerProductDto.ImageUrl);
            
            TempData["SuccessMessage"] = "Produto criado com sucesso!";
            ModelState.Clear();
            return View(new RegisterProductDTO { Suppliers = registerProductDto.Suppliers });
        }
        catch (NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            ModelState.Clear();
            return View(new RegisterProductDTO { Suppliers = registerProductDto.Suppliers });
        }
        catch (InvalidExtensionException e)
        {
            TempData["ErrorMessage"] = e.Message;
            ModelState.Clear();
            return View(new RegisterProductDTO { Suppliers = registerProductDto.Suppliers });
        }
        catch(Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            ModelState.Clear();
            return View(new RegisterProductDTO { Suppliers = registerProductDto.Suppliers });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        var suppliers = await _supplierRepository.GetAllAsync();
        var productResult = await _productService.GetById(id);
        
        var product = _mapper.Map<UpdateProductDTO>(productResult);
        
        product.Suppliers = suppliers;
        
        return View(product);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(
        [FromForm] UpdateProductDTO updateProductDto, 
        IFormFile? file)
    {
        updateProductDto.Suppliers = await _supplierRepository.GetAllAsync();
        
        if (!ModelState.IsValid) 
            return View(updateProductDto);
        
        try
        {
            var productResult = await _productService.GetById(updateProductDto.Id);

            if (file != null)
            {
                updateProductDto.ImageUrl = _imageService.GenerateImageName(file, _webHostEnvironment.WebRootPath);
                await _imageService.SaveImage(file, _webHostEnvironment.WebRootPath, updateProductDto.ImageUrl);
            }
            else
                updateProductDto.ImageUrl = productResult.Image;
            
            await _productService.Update(updateProductDto);
            TempData["SuccessMessage"] = "Produto alterado com sucesso!";
            
            return View(updateProductDto);
        }
        catch (NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("GetAll", "Product");
        }
        catch (InvalidExtensionException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("GetAll", "Product");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            var product = _mapper.Map<DeleteProductDTO>(await _productService.GetById(id));
            
            var supplier = await _supplierRepository.GetByIdAsync(product.SupplierId);
        
            if (supplier is null) throw new NotFoundException("Fornecedor não encontrado!");
        
            product.Supplier = supplier;

            return View(product);
        }
        catch (NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            return RedirectToAction("GetAll", "Product");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("GetAll", "Product");
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] DeleteProductDTO deleteProductDto)
    {
        if (!ModelState.IsValid) return View();
        
        try
        {
            await _productService.Delete(deleteProductDto);
            TempData["SuccessMessage"] = "Produto deletado com sucesso!";

            return RedirectToAction("GetAll", "Product");
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            return RedirectToAction("GetAll", "Product");
        }
    }
}