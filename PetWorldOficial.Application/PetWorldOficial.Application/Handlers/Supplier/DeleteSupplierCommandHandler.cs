using MediatR;
using PetWorldOficial.Application.Commands.Supplier;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Supplier;

public class DeleteSupplierCommandHandler(
    ISupplierService supplierService) : IRequestHandler<DeleteSupplierCommand, DeleteSupplierCommand>
{
    public async Task<DeleteSupplierCommand> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                var supplier = await supplierService.GetByIdAsync(request.Id, cancellationToken);

                if (supplier is null)
                    throw new SupplierNotFoundException("Ocorreu um problema. Fornecedor não encontrado!");

                request.Name = supplier.Name;
                request.Email = supplier.Email;
                request.CNPJ = supplier.CNPJ;
                request.CellPhone = supplier.CellPhone;
                request.Street = supplier.Street;
                request.Number = supplier.Number;
                request.Neighborhood = supplier.Neighborhood;
                request.Complement = supplier.Complement;
                request.City = supplier.City;
                request.State = supplier.State;

                return request;
            }

            await supplierService.DeleteAsync(request, cancellationToken);
            request.Message = "Fornecedor deletado com sucesso!";
            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }
}