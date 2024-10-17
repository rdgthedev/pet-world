using PetWorldOficial.Application.Commands.Animal;
using PetWorldOficial.Application.ViewModels.Animal;

namespace PetWorldOficial.Application.Services.Interfaces;

public interface IAnimalService
{
    public Task<IEnumerable<AnimalDetailsViewModel>> GetAll(CancellationToken cancellationToken);
    public Task<AnimalDetailsViewModel?> GetById(int id, CancellationToken cancellationToken);
    public Task Update(UpdateAnimalCommand command, CancellationToken cancellationToken);
    public Task Delete(DeleteAnimalCommand command, CancellationToken cancellationToken);
    public Task Create(RegisterAnimalCommand command, CancellationToken cancellationToken);
    public Task<IEnumerable<AnimalDetailsViewModel?>> GetByUserIdWithOwnerAndRace(int id, CancellationToken cancellationToken);
    public Task<IEnumerable<AnimalDetailsViewModel?>> GetWithOwnerAndRace(CancellationToken cancellationToken);
    public Task<AnimalDetailsViewModel?> GetByIdWithOwnerAndCategoryAndRaceAsync(int id, CancellationToken cancellationToken);
}