namespace PetWorldOficial.Domain.Exceptions;

public class MaximumBookingsPerAnimalExceededException : Exception
{
    public MaximumBookingsPerAnimalExceededException(string message) : base(message)
    {
        
    }
}