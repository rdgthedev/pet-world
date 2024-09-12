using MediatR;
using PetWorldOficial.Application.Queries.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Schedule;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Schedule;

public class GetAllSchedulesQueryHandler(
    IScheduleService scheduleService,
    IUserService userService,
    IAnimalService animalService) : IRequestHandler<GetAllSchedulesQuery, IEnumerable<ScheduleDetailsViewModel>>
{
    public async Task<IEnumerable<ScheduleDetailsViewModel>> Handle(
        GetAllSchedulesQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await userService.GetByUserNameAsync(request.UserPrincipal.Identity?.Name!, cancellationToken);

            if (user is null)
                throw new UserNotFoundException("Ocorreu um erro. Tente fazer o login novamente no site." +
                                                " Possívelmente sua sessão foi expirada!");

            var isAdmin = request.UserPrincipal.IsInRole(ERole.Admin.ToString());

            if (isAdmin)
                return await scheduleService.GetAll(cancellationToken)
                       ?? throw new ScheduleNotFoundException("Nenhum agendamento encontrado!");

            var animalsIds = (await animalService.GetByUserIdWithOwnerAndRace(user.Id, cancellationToken))
                .Select(a => a!.Id).ToList();

            if (!animalsIds.Any())
                throw new ScheduleNotFoundException("Nenhum agendamento encontrado!");

            var schedules = (await scheduleService
                .GetAllByAnimalsIds(animalsIds, cancellationToken)).ToList();

            if (!schedules.Any() || schedules is null)
                throw new ScheduleNotFoundException("Nenhum agendamento encontrado!");

            return schedules;
        }
        catch (Exception)
        {
            throw;
        }
    }
}