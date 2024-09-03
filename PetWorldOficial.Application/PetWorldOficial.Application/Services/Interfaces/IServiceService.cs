using PetWorldOficial.Application.DTOs.Service;
using PetWorldOficial.Application.DTOs.Service.Output;
using PetWorldOficial.Application.ViewModels.Service;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IServiceService
{
    Task<IEnumerable<OutputServiceDTO>> GetAll(CancellationToken cancellationToken);
    Task<OutputServiceDTO> GetById(int id, CancellationToken cancellationToken);
    Task<OutputServiceDTO> GetByName(string name, CancellationToken cancellationToken);
    Task Create(CreateServiceDTO createServiceDto, CancellationToken cancellationToken);
    Task Update(UpdateServiceViewModel model, CancellationToken cancellationToken);
    Task Delete(DeleteServiceViewModel createServiceDto, CancellationToken cancellationToken);
}