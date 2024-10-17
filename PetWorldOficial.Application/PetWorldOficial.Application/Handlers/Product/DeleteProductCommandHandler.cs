using MediatR;
using PetWorldOficial.Application.Commands.Product;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Product
{
    public class DeleteProductCommandHandler(
        IProductService productService) : IRequestHandler<DeleteProductCommand, DeleteProductCommand>
    {
        public async Task<DeleteProductCommand> Handle(DeleteProductCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name.Trim()))
                {
                    var product = await productService.GetById(request.Id, cancellationToken);

                    if (product is null)
                        throw new ProductNotFoundException("Produto não encontrado!");

                    request.Name = product.Name;
                    request.Description = product.Description;
                    request.CategoryName = product.CategoryName;
                    request.SupplierName = product.SupplierName;
                    request.Price = product.Price;
                    request.ImageUrl = product.ImageUrl;
                    request.SupplierId = product.SupplierId;
                    request.CategoryId = product.CategoryId;

                    return request;
                }

                await productService.Delete(request, cancellationToken);

                request.Message = "Produto deletado com sucesso!";

                return request;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}