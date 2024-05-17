namespace PetWorldOficial.Domain.Exceptions;

public class ServiceAlreadyExistsException : Exception
{
    public ServiceAlreadyExistsException(string message) : base(message)
    {
        
    }
}