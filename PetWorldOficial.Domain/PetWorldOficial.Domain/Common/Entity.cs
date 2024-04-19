using Flunt.Notifications;

namespace PetWorldOficial.Domain.Common;

public abstract class Entity : Notifiable<Notification>
{
    public Entity()
    {
    }
    
    public int Id { get; private set; }
}