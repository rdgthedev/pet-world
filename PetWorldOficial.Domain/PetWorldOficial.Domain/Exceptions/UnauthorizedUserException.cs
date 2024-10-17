namespace PetWorldOficial.Domain.Exceptions;

public class UnauthorizedUserException : Exception
{
    public UnauthorizedUserException(string message) : base(message)
    {
        
    }
}