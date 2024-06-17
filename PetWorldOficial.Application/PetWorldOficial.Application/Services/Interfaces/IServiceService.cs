using PetWorldOficial.Application.DTOs.Service;
using PetWorldOficial.Application.DTOs.Service.Output;
using PetWorldOficial.Application.ViewModels.Service;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IServiceService
{
    Task<IEnumerable<OutputServiceDTO>> GetAll();
    Task<OutputServiceDTO> GetById(int id);
    Task<OutputServiceDTO> GetByName(string name);
    Task Create(CreateServiceDTO createServiceDto);
    Task Update(UpdateServiceViewModel model);
    Task Delete(DeleteServiceViewModel createServiceDto);
}