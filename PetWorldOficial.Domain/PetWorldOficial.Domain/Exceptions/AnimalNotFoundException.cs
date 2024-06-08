using System.Linq.Expressions;

namespace PetWorldOficial.Domain.Exceptions;

public class AnimalNotFoundException : Exception
{
    public AnimalNotFoundException(string message) : base(message)
    {
    }
}