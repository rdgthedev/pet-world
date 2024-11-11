// using AutoMapper;
// using PetWorldOficial.Application.Commands.Role;
// using PetWorldOficial.Application.Services.Interfaces;
// using PetWorldOficial.Application.ViewModels.Race;
// using PetWorldOficial.Application.ViewModels.Role;
// using PetWorldOficial.Domain.Interfaces.Repositories;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
//
// namespace PetWorldOficial.Application.Services.Implementations
// {
//     public class RaceService : IRaceService
//     {
//         private readonly IRaceRepository _raceRepository;
//         private readonly IMapper _mapper;
//         public RaceService(
//             IRaceRepository raceRepository,
//             IMapper mapper)
//         {
//             _raceRepository = raceRepository;
//             _mapper = mapper;
//         }
//
//         public async Task<IEnumerable<RaceDetailsViewModel>> GetAll(CancellationToken cancellationToken)
//         {
//             return _mapper.Map<IEnumerable<RaceDetailsViewModel>>(await _raceRepository.GetAllAsync(cancellationToken));
//         }
//
//         public Task<RaceDetailsViewModel?> GetById(int id, CancellationToken cancellationToken)
//         {
//             throw new NotImplementedException();
//         }
//     }
// }