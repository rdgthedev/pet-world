namespace PetWorldOficial.Domain.Exceptions;

public class ServiceNotFoundException : Exception
{
    public ServiceNotFoundException(string message) : base(message)
    {
    }
}