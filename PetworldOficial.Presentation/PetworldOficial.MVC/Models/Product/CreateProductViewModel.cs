using PetWorldOficial.Application.DTOs.Product;
using PetWorldOficial.Domain.Entities;

namespace PetworldOficial.MVC.Models.Product;

public class CreateProductViewModel
{
    public RegisterProductDTO RegisterProduct { get; set; }
    public IEnumerable<Supplier>? Suppliers { get; set; }
}