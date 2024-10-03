using MediatR;
using Microsoft.Extensions.Options;
using PetWorldOficial.Application.Queries.Schedule;
using PetWorldOficial.Application.Services.Interfaces;
using PetWorldOficial.Application.Utils;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.Interfaces.Repositories;

namespace PetWorldOficial.Application.Handlers.Schedule;

public class GetAvailableTimesQueryHandler(
    IScheduleService scheduleService,
    IUserService userService,
    IServiceService serviceService,
    IScheduleRepository scheduleRepository,
    IOptions<Settings.Settings> options) : IRequestHandler<GetAvailableTimesQuery, IEnumerable<TimeSpan>>
{
    private readonly int _defaultRangeGrooming = options.Value.Range.DefaultRangeGrooming;
    private readonly int _defaultRangeVeterinary = options.Value.Range.DefaultRangeVeterinary;

    public async Task<IEnumerable<TimeSpan>> Handle(GetAvailableTimesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var defaultRangeAndCategoryDto = Validation.GetDefaultRangeAndCategoryType(
                request.CategoryName,
                _defaultRangeGrooming,
                _defaultRangeVeterinary);

            var timesInUse = (await scheduleRepository.GetByCategoryAndDate(defaultRangeAndCategoryDto.CategoryType,
                    request.Date, cancellationToken))
                .Select(s => s.Time)
                .GroupBy(t => t)
                .Select(g => new { Time = g.Key, Count = g.Count() })
                .ToList();

            var availableTimes = AvailableTimes.Generate(defaultRangeAndCategoryDto.DefaultRange);

            var times = availableTimes.Where(t => !timesInUse.Any(g => g.Time == t && g.Count >= 3));

            if (request.DurationInMinutes / 2 == defaultRangeAndCategoryDto.DefaultRange)
                times = ConsecutiveTimes.Get(times.ToList(), defaultRangeAndCategoryDto.DefaultRange);

            return times;
        }
        catch (Exception)
        {
            throw;
        }
    }
}