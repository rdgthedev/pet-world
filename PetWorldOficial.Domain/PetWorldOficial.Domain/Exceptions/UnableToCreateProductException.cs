using System.Runtime.Intrinsics.Arm;

namespace PetWorldOficial.Domain.Exceptions;

public class UnableToCreateProductException(string message) : Exception(message)
{
}