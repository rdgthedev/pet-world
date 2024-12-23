﻿using MediatR;

namespace PetWorldOficial.Application.Commands.Product;

public record DeleteProductCommand(int Id) : IRequest<DeleteProductCommand>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string SupplierName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int SupplierId { get; set; }
    public int CategoryId { get; set; }
    public string Message { get; set; } = string.Empty;
}