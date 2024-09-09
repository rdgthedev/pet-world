using AutoMapper;
using PetWorldOficial.Application.ViewModels.Schedule;

namespace PetWorldOficial.Application.Mappers.Schedule;

public class ViewModelsToScheduleEntityProfiles : Profile
{
    // public ViewModelsToScheduleEntityProfiles()
    // {
    //     CreateMap<CreateScheduleViewModel, Domain.Entities.Schedule>()
    //         .ConstructUsing(vm =>
    //             new Domain.Entities.Schedule(
    //                 (int)vm.AnimalId!,
    //                 vm.ServiceId,
    //                 (DateTime)vm.Date!,
    //                 (TimeSpan)vm.Time!,
    //                 vm.Observation));
    //
    //     CreateMap<DeleteScheduleViewModel, Domain.Entities.Schedule>()
    //         .ConstructUsing(vm =>
    //             new Domain.Entities.Schedule(
    //                 vm.AnimalId,
    //                 vm.ServiceId,
    //                 (DateTime)vm.Date!,
    //                 (TimeSpan)vm.Time!,
    //                 null));
    //
    //     CreateMap<UpdateScheduleViewModel, Domain.Entities.Schedule>()
    //         .ConstructUsing(vm =>
    //             new Domain.Entities.Schedule(
    //                 vm.AnimalId,
    //                 vm.ServiceId,
    //                 (DateTime)vm.Date!,
    //                 (TimeSpan)vm.Time!,
    //                 vm.Observation));
    // }
}