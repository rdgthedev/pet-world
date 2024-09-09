namespace PetWorldOficial.Application.ViewModels.Animal;

[Serializable]
public class AnimalDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public string Race { get; set; }
    public string Gender { get; set; }
    public int UserId { get; set; }

    public Domain.Entities.User? User { get; set; }
}