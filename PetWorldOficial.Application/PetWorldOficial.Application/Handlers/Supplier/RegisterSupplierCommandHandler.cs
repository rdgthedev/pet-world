using MediatR;
using PetWorldOficial.Application.Commands.Supplier;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Supplier;

public class RegisterSupplierCommandHandler(
    ISupplierService supplierService) : IRequestHandler<RegisterSupplierCommand, RegisterSupplierCommand>
{
    public async Task<RegisterSupplierCommand> Handle(RegisterSupplierCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var supplier = await supplierService.SupplierExists(
                request.Name, 
                request.CNPJ,
                request.CellPhone, cancellationToken);

            if (supplier is not null)
                throw new SupplierAlreadyExistsException(
                    "Já há um fornecedor utilizando alguma dessas informações: nome, cnpj ou telefone." +
                    " Verifique esses campos!");

            await supplierService.CreateAsync(request, cancellationToken);
            request.Message = "Fornecedor cadastrado com sucesso!";
            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }
}