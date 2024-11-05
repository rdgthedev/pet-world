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

            var isAdmin = request.UserPrincipal.IsInRole(ERole.Admin.ToString());

            if (isAdmin)
                return await scheduleService.GetAll(cancellationToken) ?? [];

            var animalsIds = (await animalService.GetByUserIdWithOwnerAndRace(user.Id, cancellationToken))
                .Select(a => a!.Id).ToList() ?? [];

            if (!animalsIds.Any())
                return Enumerable.Empty<ScheduleDetailsViewModel>();

            var schedulings = await scheduleService.GetAllByAnimalsIds(animalsIds, cancellationToken);

            return schedulings.ToList() ?? [];
        }
        catch (Exception)
        {
            throw;
        }
    }
}