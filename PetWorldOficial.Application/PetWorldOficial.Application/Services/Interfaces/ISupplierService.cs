using PetWorldOficial.Application.Commands.Product;
using PetWorldOficial.Application.Commands.Supplier;
using PetWorldOficial.Application.ViewModels.Supplier;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface ISupplierService
{
    Task<IEnumerable<SupplierDetailsViewModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<SupplierDetailsViewModel?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<SupplierDetailsViewModel?> GetByName(string name, CancellationToken cancellationToken);
    Task<SupplierDetailsViewModel?> SupplierExists(string name, string cnpj, string cellPhone, CancellationToken cancellationToken);
    Task CreateAsync(RegisterSupplierCommand command, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateSupplierCommand command, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteSupplierCommand command, CancellationToken cancellationToken);
}