namespace PetWorldOficial.Domain.Exceptions;

public class UnableToRegisterUserException : Exception
{
    public UnableToRegisterUserException(string message = "Não foi possível cadastrar o usuário!") : base(message)
    {  
    }
}