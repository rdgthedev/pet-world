using AutoMapper;
using PetWorldOficial.Application.Commands.Service;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Service;
using PetWorldOficial.Domain.Entities;
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

    public async Task<IEnumerable<ServiceDetailsViewModel>> GetAll(CancellationToken cancellationToken)
    {
        var services = _mapper.Map<IEnumerable<ServiceDetailsViewModel>>(await _serviceRepository.GetAllAsync(cancellationToken));

        return services;
    }

    public async Task<ServiceDetailsViewModel?> GetById(int id, CancellationToken cancellationToken)
    {
        var service = _mapper.Map<ServiceDetailsViewModel>(await _serviceRepository.GetByIdAsync(id, cancellationToken));

        return service;
    }

    public async Task<ServiceDetailsViewModel> GetByName(string name, CancellationToken cancellationToken)
    {
        var serviceResult = await _serviceRepository.GetByNameAsync(name, cancellationToken);
        var service = _mapper.Map<ServiceDetailsViewModel>(serviceResult);
        return service;
    }

    public async Task Create(CreateServiceCommand command, CancellationToken cancellationToken)
    {
        await _serviceRepository.CreateAsync(_mapper.Map<Service>(command), cancellationToken);
    }

    public async Task Update(
        UpdateServiceCommand command,
        CancellationToken cancellationToken)
    {
        await _serviceRepository.UpdateAsync(_mapper.Map<Service>(command), cancellationToken);
    }

    public async Task Delete(DeleteServiceCommand command, CancellationToken cancellationToken)
    {
        await _serviceRepository.DeleteAsync(_mapper.Map<Service>(command), cancellationToken);
    }
}