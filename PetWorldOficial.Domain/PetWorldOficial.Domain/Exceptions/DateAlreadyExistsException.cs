namespace PetWorldOficial.Domain.Exceptions;

public class DateAlreadyExistsException : Exception
{
    public DateAlreadyExistsException(string message) : base(message)
    {
        
    }
}