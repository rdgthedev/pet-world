﻿using AutoMapper;
using PetWorldOficial.Application.Commands.Product;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Product;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class ProductService(
    IProductRepository productRepository,
    IMapper mapper) : IProductService
{
    public async Task<IEnumerable<ProductDetailsViewModel>> GetAll(CancellationToken cancellationToken)
        => mapper.Map<IEnumerable<ProductDetailsViewModel>>(await productRepository.GetAllAsync(cancellationToken));

    public async Task<ProductDetailsViewModel> GetById(int id, CancellationToken cancellationToken)
        => mapper.Map<ProductDetailsViewModel>(await productRepository.GetByIdAsync(id, cancellationToken));

    public async Task<ProductDetailsViewModel> GetByName(string productName, CancellationToken cancellationToken)
        => mapper.Map<ProductDetailsViewModel>(await productRepository.GetByNameAsync(productName, cancellationToken));

    public async Task Create(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(command);
        await productRepository.CreateAsync(product, cancellationToken);
    }

    public async Task Update(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(command);
        await productRepository.UpdateAsync(product, cancellationToken);
    }

    public async Task Delete(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(command);
        await productRepository.DeleteAsync(product, cancellationToken);
    }
}