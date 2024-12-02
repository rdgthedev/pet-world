using System.Security.Claims;
using AutoMapper;
using MediatR;
using PetWorldOficial.Application.Commands.Scheduling;
using PetWorldOficial.Application.Queries.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.ViewModels.Schedule;
using PetWorldOficial.Domain.Entities;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Exceptions;

namespace PetWorldOficial.Application.Handlers.Scheduling;

public class GetAllSchedulesQueryHandler(
    IScheduleService scheduleService,
    IUserService userService,
    IAnimalService animalService,
    IMapper mapper) : IRequestHandler<GetAllSchedulesQuery, IEnumerable<ScheduleDetailsViewModel>>
{
    public async Task<IEnumerable<ScheduleDetailsViewModel>> Handle(
        GetAllSchedulesQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var email = request.UserPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            var user = await userService.GetByEmailAsync(
                email!,
                cancellationToken);

            if (user is null)
                throw new UserNotFoundException("Ocorreu um erro. Tente fazer o login novamente no site." +
                                                " Possívelmente sua sessão foi expirada!");

            var isAdmin = request.UserPrincipal.IsInRole(ERole.Admin.ToString());

            IEnumerable<ScheduleDetailsViewModel> schedulingsResult = [];
            var schedulingsUpdated = new List<ScheduleDetailsViewModel>();

            if (isAdmin)
            {
                schedulingsResult = await scheduleService.GetAll(cancellationToken);

                schedulingsUpdated = await CancelExpiredSchedulings(
                    schedulingsResult.ToList(),
                    cancellationToken);

                schedulingsResult = GroupSchedulings(schedulingsUpdated.ToList());

                return schedulingsResult is null || !schedulingsResult.Any()
                    ? throw new ScheduleNotFoundException("Nenhum agendamento encontrado!")
                    : schedulingsResult
                        .OrderBy(s => s.Status != ESchedullingStatus.Pending.ToString())
                        .ThenBy(s => s.Status)
                        .ThenByDescending(s => s.Date)
                        .ToList();
            }

            var animalsIds = (await animalService.GetByOwnerId(user.Id, cancellationToken))
                .Select(a => a!.Id).ToList();

            animalsIds = animalsIds is null || !animalsIds.Any()
                ? throw new ScheduleNotFoundException("Nenhum agendamento encontrado!")
                : animalsIds;

            schedulingsResult = await scheduleService.GetAllByAnimalsIds(animalsIds, cancellationToken);

            schedulingsUpdated = await CancelExpiredSchedulings(
                schedulingsResult.ToList(),
                cancellationToken);

            schedulingsResult = GroupSchedulings(schedulingsUpdated.ToList());

            return schedulingsResult is null || !schedulingsResult.Any()
                ? throw new ScheduleNotFoundException("Nenhum agendamento encontrado!")
                : schedulingsResult
                    .OrderBy(s => s.Status != ESchedullingStatus.Pending.ToString())
                    .ThenBy(s => s.Status)
                    .ThenByDescending(s => s.Date)
                    .ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }

    private List<ScheduleDetailsViewModel> GroupSchedulings(List<ScheduleDetailsViewModel> listOfSchedulings)
    {
        var schedulingsToRemove = new List<ScheduleDetailsViewModel>();

        foreach (var scheduling in listOfSchedulings.ToList())
        {
            var indexAHead = listOfSchedulings.IndexOf(scheduling) + 1;

            if (indexAHead <= listOfSchedulings.Count - 1)
                if (scheduling.Code == listOfSchedulings[indexAHead].Code)
                    schedulingsToRemove.Add(listOfSchedulings[indexAHead]);
        }

        listOfSchedulings = listOfSchedulings
            .Where(s => !schedulingsToRemove.Contains(s))
            .ToList();

        return listOfSchedulings;
    }

    private async Task<List<ScheduleDetailsViewModel>> CancelExpiredSchedulings(
        List<ScheduleDetailsViewModel> schedullings,
        CancellationToken cancellationToken)
    {
        var expiredSchedulings = mapper.Map<List<Schedulling>>(schedullings)
            .Where(s => s.Date < DateTime.Today && s.Status == ESchedullingStatus.Pending)
            .ToList();

        var schedulingsToUpdate = new List<Schedulling>();

        foreach (var scheduling in expiredSchedulings)
        {
            scheduling.UpdateStatusToCanceled();
            schedulingsToUpdate.Add(scheduling);
        }

        if (!expiredSchedulings.Any())
            return schedullings;

        await scheduleService.UpdateRange(
            mapper.Map<List<UpdateSchedulingCommand>>(expiredSchedulings),
            cancellationToken);

        foreach (var scheduling in schedullings.ToList())
        {
            var schedulingToUpdate = schedulingsToUpdate?.FirstOrDefault(s => s.Id == scheduling.Id);

            if (schedulingToUpdate is not null)
            {
                schedullings.Remove(scheduling);
                schedullings.Add(mapper.Map<ScheduleDetailsViewModel>(schedulingToUpdate));
            }
        }

        return schedullings;
    }
}