using MediatR;
using PetWorldOficial.Application.Commands.Supplier;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Supplier;

public class UpdateSupplierCommandHandler(
    ISupplierService supplierService) : IRequestHandler<UpdateSupplierCommand, UpdateSupplierCommand>
{
    public async Task<UpdateSupplierCommand> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
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
                request.PostalCode = supplier.PostalCode;
                request.State = supplier.State;

                return request;
            }

            await supplierService.UpdateAsync(request, cancellationToken);
            request.Message = "Fornecedor alterado com sucesso!";
            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }
}