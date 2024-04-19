using Flunt.Notifications;
using Flunt.Validations;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.ValueObjects;

public class Gender : Notifiable<Notification>
{
    public Gender(EGender type)
    {
        Type = type;
        AddNotifications(
            new Contract<Gender>()
                .Requires()
                .IsNotNullOrEmpty(
                    Type.ToString(),
                    "Gender.Type",
                    "O campo documento não pode ser vázio!"));
    }

    public EGender Type { get; private set; }
}