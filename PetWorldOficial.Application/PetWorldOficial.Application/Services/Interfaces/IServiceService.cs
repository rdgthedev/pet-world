using PetWorldOficial.Application.Commands.Service;
using PetWorldOficial.Application.DTOs.Service;
using PetWorldOficial.Application.DTOs.Service.Output;
using PetWorldOficial.Application.ViewModels.Service;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IServiceService
{
    Task<IEnumerable<ServiceDetailsViewModel>> GetAll(CancellationToken cancellationToken);
    Task<ServiceDetailsViewModel?> GetById(int id, CancellationToken cancellationToken);
    Task<OutputServiceDTO> GetByName(string name, CancellationToken cancellationToken);
    Task Create(CreateServiceCommand command, CancellationToken cancellationToken);
    Task Update(UpdateServiceCommand command, CancellationToken cancellationToken);
    Task Delete(DeleteServiceCommand command, CancellationToken cancellationToken);
}