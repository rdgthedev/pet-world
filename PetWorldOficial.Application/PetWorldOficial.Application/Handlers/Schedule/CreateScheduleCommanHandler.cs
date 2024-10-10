// using MediatR;
// using Microsoft.Extensions.Options;
// using PetWorldOficial.Application.Commands.Schedule;
// using PetWorldOficial.Application.DTO;
// using PetWorldOficial.Application.Services.Interfaces;
// using PetWorldOficial.Application.Utils;
// using PetWorldOficial.Domain.Entities;
// using PetWorldOficial.Domain.Exceptions;
// using PetWorldOficial.Domain.Interfaces.Repositories;
//
// namespace PetWorldOficial.Application.Handlers.Schedule;
//
// public class CreateScheduleCommanHandler(
//     IScheduleService scheduleService,
//     IUserService userService,
//     IAnimalService animalService,
//     IServiceService serviceService,
//     IScheduleRepository scheduleRepository,
//     IUserRepository userRepository,
//     IOptions<Settings.Settings> options) : IRequestHandler<CreateScheduleCommand, CreateScheduleCommand>
// {
//     private readonly int _defaultRangeGrooming = options.Value.Range.DefaultRangeFastServices;
//     private readonly int _defaultRangeVeterinary = options.Value.Range.DefaultRangeTimeConsumingServices;
//
//     public async Task<CreateScheduleCommand> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
//     {
//         try
//         {
//             if (request.Animals is null || !request.Animals.Any())
//             {
//                 var user = await userService.GetByUserNameAsync(
//                     request.UserPrincipal?.Identity?.Name!,
//                     cancellationToken);
//
//                 if (user is null)
//                     throw new UserNotFoundException("Ocorreu um erro! Por favor tente se reconectar.");
//
//                 request.Animals = await animalService.GetByUserIdWithOwnerAndRace(user.Id, cancellationToken);
//
//                 var service = await serviceService.GetById(request.ServiceId, cancellationToken);
//
//                 if (service is null)
//                     throw new ServiceNotFoundException("Serviço não encontrado!");
//
//                 request.ServiceName = service.Name;
//                 request.ServicePrice = service.Price;
//                 request.DurationInMinutes = service.DurationInMinutes;
//                 request.CategoryName = service.Category.Title;
//
//                 return request;
//             }
//
//             // var defaultRangeAndCategoryDto = Validation.GetDefaultRangeAndCategoryType(
//             //     request.CategoryName,
//             //     _defaultRangeGrooming);
//             //
//             // var roleName = RoleUtils.GetRoleByServiceName(request.ServiceName);
//             //
//             // var allEmployeesIds = (await userService
//             //         .GetUsersByRoleAsync(roleName, cancellationToken))
//             //     .Select(s => s.Id)
//             //     .ToList();
//             //
//             // if (!allEmployeesIds.Any())
//             //     throw new Exception("Nenhum funcionário com este perfil cadastrado!");
//             //
//             // var scheduledsByDate = await scheduleRepository.GetByCategoryAndDate(
//             //     defaultRangeAndCategoryDto.CategoryType,
//             //     request.Date!.Value,
//             //     cancellationToken);
//             //
//             // var scheduledEmployeesIds = GetScheduledEmployeesIds(
//             //     scheduledsByDate,
//             //     request.DurationInMinutes,
//             //     defaultRangeAndCategoryDto.DefaultRange,
//             //     request.Time!.Value);
//             //
//             // request.EmployeeId = allEmployeesIds.FirstOrDefault(i => !scheduledEmployeesIds.Contains(i));
//
//             // if (request.EmployeeId is 0)
//             //     throw new Exception("Nenhum funcionário disponível!");
//             //
//             // AddSchedullings(request, defaultRangeAndCategoryDto);
//             //
//             // await scheduleService.CreateInBatch(request, cancellationToken);
//             //
//             // request.Message = $"{request.ServiceName} agendado com sucesso!";
//             return request;
//         }
//         catch (Exception)
//         {
//             throw;
//         }
//     }
//
//     private void AddSchedullings(
//         CreateScheduleCommand request,
//         DefaultRangeAndCategoryDTO defaultRangeAndCategoryDto)
//     {
//         request.Schedullings ??= [];
//
//         if (request.DurationInMinutes / 2 == defaultRangeAndCategoryDto.DefaultRange)
//         {
//             request.Schedullings.Add(new Schedulling(
//                 request.AnimalId!.Value,
//                 request.ServiceId,
//                 request.EmployeeId!.Value,
//                 request.Date!.Value,
//                 request.Time!.Value,
//                 request.Observation));
//
//             request.Schedullings.Add(new Schedulling(
//                 request.AnimalId.Value,
//                 request.ServiceId,
//                 request.EmployeeId!.Value,
//                 request.Date!.Value,
//                 request.Time!.Value.Add(TimeSpan.FromMinutes(defaultRangeAndCategoryDto.DefaultRange)),
//                 request.Observation));
//
//             return;
//         }
//
//         request.Schedullings!.Add(new Schedulling(
//             request.AnimalId!.Value,
//             request.ServiceId,
//             request.EmployeeId!.Value,
//             request.Date!.Value,
//             request.Time!.Value,
//             request.Observation));
//     }
//
//     private List<int> GetScheduledEmployeesIds(
//         IEnumerable<Schedulling> schedullings,
//         int durationInMinutes,
//         int defaultRange,
//         TimeSpan time)
//     {
//         var scheduledEmployeesIds = new List<int>();
//
//         if (durationInMinutes / 2 == defaultRange)
//         {
//             scheduledEmployeesIds = schedullings
//                 .Where(s => s.Time == time
//                             && s.Time.Add(TimeSpan.FromDays(defaultRange)) == time.Add(TimeSpan.FromDays(defaultRange)))
//                 .Select(s => s.EmployeeId)
//                 .ToList();
//         }
//         else
//         {
//             scheduledEmployeesIds = schedullings
//                 .Where(s => s.Time == time)
//                 .Select(s => s.EmployeeId)
//                 .ToList();
//         }
//
//         return scheduledEmployeesIds;
//     }
// }