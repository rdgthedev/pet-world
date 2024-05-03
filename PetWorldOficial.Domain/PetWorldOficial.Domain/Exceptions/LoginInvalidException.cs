namespace PetWorldOficial.Domain.Exceptions;

public class LoginInvalidException : Exception
{
    public LoginInvalidException(string message) : base(message)
    {
        
    }
}