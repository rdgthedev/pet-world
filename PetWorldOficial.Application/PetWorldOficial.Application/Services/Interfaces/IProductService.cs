using PetWorldOficial.Application.DTOs.Product;
using PetWorldOficial.Application.DTOs.Product.Output;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<OutputProductDTO>> GetAll(CancellationToken cancellationToken);
    Task<OutputProductDTO> GetById(int id, CancellationToken cancellationToken);
    Task Create(RegisterProductDTO product, CancellationToken cancellationToken);
    Task Update(UpdateProductDTO product, CancellationToken cancellationToken);
    Task Delete(DeleteProductDTO product, CancellationToken cancellationToken);
}