using AutoMapper;
using PetWorldOficial.Application.DTOs.Service;
using PetWorldOficial.Application.DTOs.Service.Output;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Service;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IMapper _mapper;

    public ServiceService(
        IServiceRepository serviceRepository,
        IMapper mapper)
    {
        _serviceRepository = serviceRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OutputServiceDTO>> GetAll()
    {
        var services = _mapper.Map<IEnumerable<OutputServiceDTO>>(await _serviceRepository.GetAllAsync());
        if (services == null) throw new NotFoundException("Nenhum serviço encontrado!");
        return services;
    }

    public async Task<OutputServiceDTO> GetById(int id)
    {
        var service = _mapper.Map<OutputServiceDTO>(await _serviceRepository.GetByIdAsync(id));
        if (service == null) throw new NotFoundException("Serviço não encontrado!");
        return service;
    }

    public async Task<OutputServiceDTO> GetByName(string name)
    {
        var service = _mapper.Map<OutputServiceDTO>(await _serviceRepository.GetByNameAsync(name));
        return service;
    }

    public async Task Create(CreateServiceDTO createServiceDto)
    {
        var service = _mapper.Map<Service>(createServiceDto);
        await _serviceRepository.CreateAsync(service);
    }

    public async Task Update(UpdateServiceViewModel model)
    {
        var service = await _serviceRepository.GetByIdAsync(model.Id);

        if (service is null)
            throw new ServiceNotFoundException("Serviço não encontrado!");

        await _serviceRepository.UpdateAsync(_mapper.Map(model, service));
    }

    public async Task Delete(DeleteServiceViewModel model)
    {
        var service = await _serviceRepository.GetByIdAsync(model.Id);

        if (service is null)
            throw new ServiceNotFoundException("Serviço não encontrado!");
        
        await _serviceRepository.DeleteAsync(_mapper.Map(model, service));
    }
}