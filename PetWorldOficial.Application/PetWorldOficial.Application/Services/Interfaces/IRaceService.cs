using PetWorldOficial.Application.ViewModels.Race;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetWorldOficial.Application.Services.Interfaces
{
    public interface IRaceService
    {
        Task<IEnumerable<RaceDetailsViewModel>> GetAll(CancellationToken cancellationToken);
        Task<RaceDetailsViewModel?> GetById(int id, CancellationToken cancellationToken);
        //Task Create(RaceDetailsViewModel product, CancellationToken cancellationToken);
        //Task Update(UpdateProductDTO product, CancellationToken cancellationToken);
        //Task Delete(DeleteProductDTO product, CancellationToken cancellationToken);
    }
}