using MediatR;
using PetWorldOficial.Application.ViewModels.Supplier;

namespace PetWorldOficial.Application.Queries.Supplier;

public class GetAllSuppliersQuery : IRequest<IEnumerable<SupplierDetailsViewModel>>
{
    
}