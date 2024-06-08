namespace PetWorldOficial.Domain.Exceptions;

public class ScheduleNotFoundException : Exception
{
    public ScheduleNotFoundException(string message) : base(message)
    {
    }
}