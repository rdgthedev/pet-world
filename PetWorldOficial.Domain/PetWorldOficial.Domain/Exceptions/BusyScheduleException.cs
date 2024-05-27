namespace PetWorldOficial.Domain.Exceptions;

public class BusyScheduleException : Exception
{
    public BusyScheduleException(string message) : base(message)
    {
        
    }
}