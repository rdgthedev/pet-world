using Stripe;

namespace PetWorldOficial.Domain.Exceptions;

public class UnableToResetPasswordException(string message) : Exception(message)
{
    
}