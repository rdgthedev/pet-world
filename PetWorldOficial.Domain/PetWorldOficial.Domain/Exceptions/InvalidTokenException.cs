namespace PetWorldOficial.Domain.Exceptions;

public class InvalidTokenException(string message) : Exception(message)
{
}