using PetWorldOficial.Application.DTOs.Product;
using PetWorldOficial.Application.DTOs.Product.Output;
using PetWorldOficial.Domain.Entities;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<OutputProductDTO>> GetAll();
    Task<OutputProductDTO> GetById(int id);
    Task Create(RegisterProductDTO product);
    Task Update(UpdateProductDTO product);
    Task Delete(Product product);
}