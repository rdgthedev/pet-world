using AutoMapper;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Services.Implementations
{
    public class CategoryService(
        ICategoryRepository categoryRepository,
        IMapper mapper) : ICategoryService
    {
        public async Task<IEnumerable<CategoryDetailsViewModel>> GetAll(CancellationToken cancellationToken)
        {
            return mapper.Map<IEnumerable<CategoryDetailsViewModel>>(
                await categoryRepository.GetAllAsync(cancellationToken));
        }

        public Task<CategoryDetailsViewModel?> GetById(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoryDetailsViewModel>> GetAllServiceCategories(
            CancellationToken cancellationToken)
            => mapper.Map<IEnumerable<CategoryDetailsViewModel>>(await categoryRepository.GetAllServiceCategories(cancellationToken));

        public async Task<IEnumerable<CategoryDetailsViewModel>> GetAllProductCategories(CancellationToken cancellationToken)
            => mapper.Map<IEnumerable<CategoryDetailsViewModel>>(await categoryRepository.GetAllProductCategories(cancellationToken));
    }
}