using AutoMapper;
using PetWorldOficial.Application.Commands.Product;
using PetWorldOficial.Application.Commands.Supplier;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Supplier;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class SupplierService(
    IMapper mapper,
    ISupplierRepository supplierRepository) : ISupplierService
{
    public async Task<IEnumerable<SupplierDetailsViewModel>> GetAllAsync(CancellationToken cancellationToken)
        => mapper.Map<IEnumerable<SupplierDetailsViewModel>>(await supplierRepository.GetAllAsync(cancellationToken));

    public async Task<SupplierDetailsViewModel?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => mapper.Map<SupplierDetailsViewModel?>(await supplierRepository.GetByIdAsync(id, cancellationToken));

    public async Task<SupplierDetailsViewModel?> GetByName(string name, CancellationToken cancellationToken)
        => mapper.Map<SupplierDetailsViewModel?>(await supplierRepository.GetByNameAsync(name, cancellationToken));

    public async Task<SupplierDetailsViewModel?> SupplierExists(
        string name, 
        string cnpj,
        string cellPhone,
        CancellationToken cancellationToken)
        => mapper.Map<SupplierDetailsViewModel?>(await supplierRepository.ExistsAsync(name, cnpj, cellPhone, cancellationToken));

    public Task CreateAsync(RegisterSupplierCommand command, CancellationToken cancellationToken)
        => supplierRepository.CreateAsync(mapper.Map<Supplier>(command), cancellationToken);

    public Task UpdateAsync(UpdateSupplierCommand command, CancellationToken cancellationToken)
        => supplierRepository.UpdateAsync(mapper.Map<Supplier>(command), cancellationToken);

    public Task DeleteAsync(DeleteSupplierCommand command, CancellationToken cancellationToken)
        => supplierRepository.DeleteAsync(mapper.Map<Supplier>(command), cancellationToken);
}