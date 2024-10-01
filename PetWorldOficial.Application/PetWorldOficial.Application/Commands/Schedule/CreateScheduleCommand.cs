using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using MediatR;
using PetWorldOficial.Application.ViewModels.Animal;
using PetWorldOficial.Application.ViewModels.Service;

namespace PetWorldOficial.Application.Commands.Schedule;

public record CreateScheduleCommand(ClaimsPrincipal? UserPrincipal) : IRequest<CreateScheduleCommand>
{
    [Required(ErrorMessage = "O pet é obrigatório!")]
    public int? AnimalId { get; set; }

    [Required] public int ServiceId { get; set; }

    [Required(ErrorMessage = "A Data é obrigatória")]
    [DisplayName("Data")]
    public DateTime? Date { get; set; }

    [Required(ErrorMessage = "O Horário é obrigatório")]
    [DisplayName("Horário")]
    public TimeSpan? Time { get; set; }

    [DisplayName("Obervação")] public string? Observation { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório")]
    public string ServiceName { get; set; } = string.Empty;

    public int DurationInMinutes { get; set; }

    [Required(ErrorMessage = "O preço é obrigatório")]
    public double ServicePrice { get; set; }

    public IEnumerable<AnimalDetailsViewModel?>? Animals { get; set; }
    public int? UserId { get; set; }
    public int? EmployeeId { get; set; }
    public string Message { get; set; } = string.Empty;
}