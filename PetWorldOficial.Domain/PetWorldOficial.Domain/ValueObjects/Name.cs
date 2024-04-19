using Flunt.Notifications;
using Flunt.Validations;

namespace PetWorldOficial.Domain.ValueObjects;

public class Name : Notifiable<Notification>
{
    public Name(string firstName)
    {
        FirstName = firstName;

        AddNotifications(new Contract<Name>()
            .Requires()
            .IsNotNullOrEmpty(
                FirstName,
                "Name.FirstName",
                "O campo primeiro nome não pode ser vázio!")
            .IsGreaterThan(
                FirstName.Length,
                2,
                "Name.FirstName",
                "O primeiro nome deve conter no mínimo 3 caracteres"));
    }
    
    public string FirstName { get; private set; }
}
