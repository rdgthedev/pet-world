namespace PetWorldOficial.Domain.Exceptions;

public class CartNotFoundException(string message) : Exception(message)
{
}