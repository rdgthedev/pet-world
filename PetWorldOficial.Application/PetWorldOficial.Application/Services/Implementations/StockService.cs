using AutoMapper;
using PetWorldOficial.Application.Commands.Product;
using PetWorldOficial.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetWorldOficial.Application.Handlers;

namespace PetWorldOficial.Application.Services.Implementations
{
    public class StockService(
        IStockRepository stockRepository,
        IMapper mapper) : IStockService
    {
        public async Task CreateAsync(CreateProductCommand command, CancellationToken cancellationToken)
            => await stockRepository.CreateAsync(mapper.Map<Domain.Entities.Stock>(command), cancellationToken);

        public async Task UdpateAsync(UpdateProductCommand command, CancellationToken cancellationToken)
            => await stockRepository.UpdateAsync(mapper.Map<Domain.Entities.Stock>(command), cancellationToken);
    }
}