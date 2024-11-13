using MediatR;
using PetWorldOficial.Application.Queries.Supplier;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Supplier;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Supplier;

public class GetAllSupplierQueryHandler(
    ISupplierService supplierService) : IRequestHandler<GetAllSuppliersQuery, IEnumerable<SupplierDetailsViewModel>>
{
    public async Task<IEnumerable<SupplierDetailsViewModel>> Handle(GetAllSuppliersQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var suppliers = await supplierService.GetAllAsync(cancellationToken);

            if (suppliers is null || !suppliers.Any())
                throw new SupplierNotFoundException("Nenhum supplier encontrado!");

            return suppliers; 
        }
        catch (Exception)
        {
            throw;
        }
    }
}