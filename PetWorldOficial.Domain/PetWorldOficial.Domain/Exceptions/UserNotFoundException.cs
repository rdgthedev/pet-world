namespace PetWorldOficial.Domain.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string message) : base(message)
    {
    }
}