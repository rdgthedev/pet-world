using System.Security.Claims;
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
            var email = request.UserPrincipal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            var user = await userService.GetByEmailAsync(
                email!,
                cancellationToken);

            if (user is null)
                throw new UserNotFoundException("Ocorreu um erro. Tente fazer o login novamente no site." +
                                                " Possívelmente sua sessão foi expirada!");

            var isAdmin = request.UserPrincipal!.IsInRole(ERole.Admin.ToString());

            if (isAdmin)
            {
                var schedulingsResult = await scheduleService.GetAll(cancellationToken);
                schedulingsResult = GroupSchedulings(schedulingsResult.ToList());

                return schedulingsResult is null || !schedulingsResult.Any()
                    ? throw new ScheduleNotFoundException("Nenhum agendamento encontrado!")
                    : schedulingsResult;
            }


            var animalsIds = (await animalService.GetByUserIdWithOwnerAndRace(user.Id, cancellationToken))
                .Select(a => a!.Id).ToList();

            animalsIds = animalsIds is null || !animalsIds.Any()
                ? throw new ScheduleNotFoundException("Nenhum agendamento encontrado!")
                : animalsIds;

            var schedulings = await scheduleService.GetAllByAnimalsIds(animalsIds, cancellationToken);
            schedulings = GroupSchedulings(schedulings.ToList());
            return schedulings is null || !schedulings.Any()
                ? throw new ScheduleNotFoundException("Nenhum agendamento encontrado!")
                : schedulings;
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
}