using System.Globalization;
using Flunt.Notifications;
using Flunt.Validations;

namespace PetWorldOficial.Domain.ValueObjects;

public class BirthDate : Notifiable<Notification>
{
    public BirthDate(DateTime date)
    {
        Date = date;
        
        AddNotifications(
            new Contract<BirthDate>()
                .Requires()
                .IsNotNullOrEmpty(
                    Date.ToString(CultureInfo.InvariantCulture),
                    "BirthDate.Date",
                    "O campo data não pode ser vázio!"));
    }

    public DateTime Date { get; private set; }
}