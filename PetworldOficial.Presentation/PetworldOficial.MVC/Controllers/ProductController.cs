using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetWorldOficial.Application.DTOs.Product;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.ApplicationServices;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetworldOficial.MVC.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ISupplierRepository _supplierRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IImageService _imageService;
    private readonly IMapper _mapper;
    
    public ProductController(
        IProductRepository productRepository,
        ISupplierRepository supplierRepository,
        IWebHostEnvironment webHostEnvironment,
        IImageService imageService,
        IMapper mapper)
    {
        _productRepository = productRepository;
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
            var products = await _productRepository.GetAllAsync();
            if (products == null)
                throw new NotFoundException("Nenhum produto encontrado!");
            
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
        => View(new RegisterProductDTO { Suppliers = await _supplierRepository.GetAllAsync() });
    
    
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] RegisterProductDTO registerProductModel, IFormFile file)
    {
        registerProductModel.Suppliers = await _supplierRepository.GetAllAsync();

        if (!ModelState.IsValid)
        {
            if(file == null)
                ModelState.AddModelError(string.Empty, "A imagem é obrigatória!");
            
            return View(registerProductModel);
        }
        
        try
        {
            var supplier = await _supplierRepository.GetByIdAsync(registerProductModel.SupplierId);

            if (supplier == null) throw new NotFoundException("Fornecedor não encontrado!");
            
            registerProductModel.ImageUrl = _imageService.GenerateImageName(file, _webHostEnvironment.WebRootPath);

            var product = _mapper.Map<Product>(registerProductModel);
            
            await _productRepository.CreateAsync(product);
            await _imageService.SaveImage(file, _webHostEnvironment.WebRootPath, product.Image);
            
            TempData["SuccessMessage"] = "Produto criado com sucesso!";
            ModelState.Clear();
            return View(new RegisterProductDTO { Suppliers = registerProductModel.Suppliers });
        }
        catch (NotFoundException e)
        {
            TempData["ErrorMessage"] = e.Message;
            ModelState.Clear();
            return View(new RegisterProductDTO { Suppliers = registerProductModel.Suppliers });
        }
        catch (InvalidExtensionException e)
        {
            TempData["ErrorMessage"] = e.Message;
            ModelState.Clear();
            return View(new RegisterProductDTO { Suppliers = registerProductModel.Suppliers });
        }
        catch(Exception)
        {
            TempData["ErrorMessage"] = "Ocorreu um erro interno!";
            ModelState.Clear();
            return View(new RegisterProductDTO { Suppliers = registerProductModel.Suppliers });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        var productResult = await _productRepository.GetByIdAsync(id);
        var suppliers = await _supplierRepository.GetAllAsync();
        
        if (productResult is null) throw new NotFoundException("Produto não encontrado!");
        
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
            var productResult = await _productRepository.GetByIdAsync(updateProductDto.Id);
    
            if (productResult is null) throw new NotFoundException("Produto não encontrado!");

            updateProductDto.ImageUrl = productResult.Image;

            if (file != null)
            {
                updateProductDto.ImageUrl = _imageService.GenerateImageName(file, _webHostEnvironment.WebRootPath);
                await _imageService.SaveImage(file, _webHostEnvironment.WebRootPath, updateProductDto.ImageUrl);
            }

            var productUpdate = _mapper.Map<Product>(updateProductDto);
            
            await _productRepository.Update(productUpdate);
            
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
        var product = await _productRepository.GetByIdAsync(id);

        if (product is null) throw new NotFoundException("Produto não encontrado!");

        var supplier = await _supplierRepository.GetByIdAsync(product.SupplierId);
        
        if (supplier is null) throw new NotFoundException("Fornecedor não encontrado!");
        
        return View(new DeleteProductDTO(product, supplier));
    }
    
    [HttpDelete]
    public async Task<IActionResult> Delete([FromForm] Product product)
    {
        try
        {
            await _productRepository.Delete(product);
            TempData["SuccessMessage"] = "Produto deletado com sucesso!";

            return RedirectToAction("GetAll", "Product");
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
}