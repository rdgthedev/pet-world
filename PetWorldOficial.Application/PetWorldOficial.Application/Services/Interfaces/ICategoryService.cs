﻿using PetWorldOficial.Application.ViewModels;

namespace PetWorldOficial.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDetailsViewModel>> GetAll(CancellationToken cancellationToken);
        Task<CategoryDetailsViewModel?> GetById(int id, CancellationToken cancellationToken);
        Task<IEnumerable<CategoryDetailsViewModel>> GetAllServiceCategories(CancellationToken cancellationToken);
        Task<IEnumerable<CategoryDetailsViewModel>> GetAllAnimalCategories(CancellationToken cancellationToken);
        Task<IEnumerable<CategoryDetailsViewModel>> GetAllProductCategories(CancellationToken cancellationToken);
        //Task Create(RegisterProductDTO product, CancellationToken cancellationToken);
        //Task Update(UpdateProductDTO product, CancellationToken cancellationToken);
        //Task Delete(DeleteProductDTO product, CancellationToken cancellationToken);
    }
}
