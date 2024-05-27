namespace PetWorldOficial.Domain.Exceptions;

public class AnimalAlreadyExistsException : Exception
{
    public AnimalAlreadyExistsException(string message) : base(message)
    {
        
    }
}