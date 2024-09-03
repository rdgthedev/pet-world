using AutoMapper;
using PetWorldOficial.Application.Commands.Animal;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Animal;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations;

public class AnimalService(
    IAnimalRepository animalRepository,
    IMapper mapper) : IAnimalService
{
    public async Task<IEnumerable<AnimalDetailsViewModel>> GetAll(CancellationToken cancellationToken)
        => mapper.Map<IEnumerable<AnimalDetailsViewModel>>(await animalRepository.GetAllAsync(cancellationToken));

    public async Task<AnimalDetailsViewModel?> GetById(int id, CancellationToken cancellationToken)
        => mapper.Map<AnimalDetailsViewModel>(await animalRepository.GetByIdAsync(id, cancellationToken));

    public async Task Update(UpdateAnimalCommand command, CancellationToken cancellationToken)
        => await animalRepository.UpdateAsync(mapper.Map<Animal>(command), cancellationToken);

    public async Task Delete(DeleteAnimalCommand command, CancellationToken cancellationToken)
        => await animalRepository.DeleteAsync(mapper.Map<Animal>(command), cancellationToken);

    public async Task Create(RegisterAnimalCommand command, CancellationToken cancellationToken)
        => await animalRepository.CreateAsync(mapper.Map<Animal>(command), cancellationToken);

    public async Task<IEnumerable<AnimalDetailsViewModel?>> GetByUserId(int id, CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<AnimalDetailsViewModel>>(
            await animalRepository.GetByUserIdAsync(id, cancellationToken));
    }

    public async Task<IEnumerable<AnimalDetailsViewModel?>> GetWithUser(CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<AnimalDetailsViewModel>>(await animalRepository.GetWithUser(cancellationToken));
    }
}