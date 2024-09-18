namespace PetWorldOficial.Domain.Exceptions;

public class ProductAlreadyExistsException(string message) : Exception(message)
{
}