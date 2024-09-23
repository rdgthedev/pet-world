using PetWorldOficial.Application.Commands.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.Services.Interfaces
{
    public interface IStockService
    {
        Task CreateAsync(CreateProductCommand command, CancellationToken cancellationToken);
        Task UdpateAsync(UpdateProductCommand command, CancellationToken cancellationToken);
    }
}