using PetWorldOficial.Application.Commands.Product;
using PetWorldOficial.Application.ViewModels.Stock;

namespace PetWorldOficial.Application.Services.Interfaces
{
    public interface IStockService
    {
        Task CreateAsync(CreateProductCommand command, CancellationToken cancellationToken);
        Task UdpateAsync(UpdateProductCommand command, CancellationToken cancellationToken);
        Task UdpateAsync(StockDetailsViewModel stockDetailsViewModel, CancellationToken cancellationToken);
        Task<StockDetailsViewModel> GetByProductIdAsync(int productId, CancellationToken cancellationToken);
        Task<bool> ValidateStockQuantity(int id, int quantity, CancellationToken cancellationToken);
    }
}