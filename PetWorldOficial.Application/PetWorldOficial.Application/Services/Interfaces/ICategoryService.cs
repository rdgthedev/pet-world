using PetWorldOficial.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDetailsViewModel>> GetAll(CancellationToken cancellationToken);
        Task<CategoryDetailsViewModel?> GetById(int id, CancellationToken cancellationToken);
        //Task Create(RegisterProductDTO product, CancellationToken cancellationToken);
        //Task Update(UpdateProductDTO product, CancellationToken cancellationToken);
        //Task Delete(DeleteProductDTO product, CancellationToken cancellationToken);
    }
}
