using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Commands.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Settings;
using PetWorldOficial.Application.Utils;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Handlers.Schedule;

public class CreateScheduleCommanHandler(
    IScheduleService scheduleService,
    IUserService userService,
    IAnimalService animalService,
    IServiceService serviceService,
    IUserRepository userRepository,
    IOptions<Settings.Settings> options) : IRequestHandler<CreateScheduleCommand, CreateScheduleCommand>
{
    private readonly int _defaultRangeGrooming = options.Value.Range.DefaultRangeGrooming;
    private readonly int _defaultRangeVeterinary = options.Value.Range.DefaultRangeVeterinary;

    public async Task<CreateScheduleCommand> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Animals is null || !request.Animals.Any())
            {
                var user = await userService.GetByUserNameAsync(request.UserPrincipal?.Identity?.Name!,
                    cancellationToken);
                if (user is null)
                    throw new UserNotFoundException("Ocorreu um erro! Por favor tente se reconectar.");

                request.Animals = await animalService.GetByUserIdWithOwnerAndRace(user.Id, cancellationToken);

                var service = await serviceService.GetById(request.ServiceId, cancellationToken);

                if (service is null)
                    throw new ServiceNotFoundException("Serviço não encontrado!");

                request.ServiceName = service.Name;
                request.ServicePrice = service.Price;
                request.DurationInMinutes = service.DurationInMinutes;
                request.CategoryName = service.Category.Title;

                return request;
            }

            var defaultRangeAndCategoryDto = Validation.GetDefaultRangeAndCategoryType(
                request.CategoryName,
                _defaultRangeGrooming,
                _defaultRangeVeterinary);

            var schedullings = new List<Schedulling>();

            if (request.DurationInMinutes / 2 == defaultRangeAndCategoryDto.DefaultRange)
            {
                request.Schedullings.Add(new Schedulling(
                    request.AnimalId!.Value,
                    request.ServiceId,
                    request.EmployeeId!.Value,
                    request.Date!.Value,
                    request.Time!.Value,
                    request.Observation));

                request.Schedullings.Add(new Schedulling(
                    request.AnimalId.Value,
                    request.ServiceId,
                    request.EmployeeId!.Value,
                    request.Date!.Value,
                    request.Time!.Value.Add(TimeSpan.FromMinutes(defaultRangeAndCategoryDto.DefaultRange)),
                    request.Observation));
            }
            else
            {
                request.Schedullings.Add(new Schedulling(
                    request.AnimalId!.Value,
                    request.ServiceId,
                    request.EmployeeId!.Value,
                    request.Date!.Value,
                    request.Time!.Value,
                    request.Observation));
            }

            await scheduleService.Create(request, cancellationToken);

            request.Message = $"{request.ServiceName} agendado com sucesso!";
            return request;
        }
        catch (Exception)
        {
            throw;
        }
    }
}