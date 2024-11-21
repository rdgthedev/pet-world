using AutoMapper;
using PetWorldOficial.Application.Commands.Product;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Stock;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations
{
    public class StockService(
        IStockRepository stockRepository,
        IMapper mapper) : IStockService
    {
        public async Task CreateAsync(CreateProductCommand command, CancellationToken cancellationToken)
            => await stockRepository.CreateAsync(mapper.Map<Domain.Entities.Stock>(command), cancellationToken);

        public async Task UdpateAsync(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var stock = mapper.Map<Domain.Entities.Stock>(command);
            await stockRepository.UpdateAsync(stock, cancellationToken);
        }

        public async Task UdpateAsync(StockDetailsViewModel stockDetailsViewModel, CancellationToken cancellationToken)
        {
            var stockResult = await GetByProductIdAsync(stockDetailsViewModel.ProductId, cancellationToken);
            var stock = mapper.Map<Stock>(stockResult);
            await stockRepository.UpdateAsync(mapper.Map(stockDetailsViewModel, stock), cancellationToken);
        }

        public async Task<StockDetailsViewModel> GetByProductIdAsync(int productId, CancellationToken cancellationToken)
        {
            return mapper.Map<StockDetailsViewModel>(
                await stockRepository.GetByProductId(productId, cancellationToken));
        }

        public async Task<bool> ValidateStockQuantity(int productId, int quantity, CancellationToken cancellationToken)
        {
            var stock = await stockRepository.GetByProductId(productId, cancellationToken);

            if (stock is null)
                throw new Exception();

            return stock.Quantity >= quantity;
        }
    }
}