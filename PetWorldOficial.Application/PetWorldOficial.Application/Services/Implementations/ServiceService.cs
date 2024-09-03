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

    public async Task<IEnumerable<OutputServiceDTO>> GetAll(CancellationToken cancellationToken)
    {
        var services = _mapper.Map<IEnumerable<OutputServiceDTO>>(await _serviceRepository.GetAllAsync(cancellationToken));
        
        if (services == null)
            throw new NotFoundException("Nenhum serviço encontrado!");
        
        return services;
    }

    public async Task<OutputServiceDTO> GetById(int id, CancellationToken cancellationToken)
    {
        var service = _mapper.Map<OutputServiceDTO>(await _serviceRepository.GetByIdAsync(id, cancellationToken));
        
        if (service == null) 
            throw new NotFoundException("Serviço não encontrado!");
        
        return service;
    }

    public async Task<OutputServiceDTO> GetByName(string name, CancellationToken cancellationToken)
    {
        var serviceResult = await _serviceRepository.GetByNameAsync(name, cancellationToken);
        var service = _mapper.Map<OutputServiceDTO>(serviceResult);
        return service;
    }

    public async Task Create(CreateServiceDTO createServiceDto, CancellationToken cancellationToken)
    {
        var service = _mapper.Map<Service>(createServiceDto);
        await _serviceRepository.CreateAsync(service, cancellationToken);
    }

    public async Task Update(UpdateServiceViewModel model, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetByIdAsync(model.Id, cancellationToken);

        if (service is null)
            throw new ServiceNotFoundException("Serviço não encontrado!");
        
        var mappedService = _mapper.Map(model, service);
        
        await _serviceRepository.UpdateAsync(mappedService, cancellationToken);
    }

    public async Task Delete(DeleteServiceViewModel model, CancellationToken cancellationToken)
    {
        var service = await _serviceRepository.GetByIdAsync(model.Id, cancellationToken);

        if (service is null)
            throw new ServiceNotFoundException("Serviço não encontrado!");

        await _serviceRepository.DeleteAsync(service, cancellationToken);
    }
}