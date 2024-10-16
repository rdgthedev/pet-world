using MediatR;

namespace PetWorldOficial.Application.Commands.Schedule
{
    public record DeleteSchedulingCommand(int SchedulingId) : IRequest<DeleteSchedulingCommand>
    {
        public DateTime SchedulingDate { get; set; }
        public TimeSpan SchedulingTime { get; set; }
        public string AnimalName { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string ServiceName { get; set; } = string.Empty;
        public double ServicePrice { get; set; }
        public int DurationInMinutes { get; set; }
        public string? Observation { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
