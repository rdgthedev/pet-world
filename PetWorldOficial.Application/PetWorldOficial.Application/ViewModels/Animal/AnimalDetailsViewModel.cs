namespace PetWorldOficial.Application.ViewModels.Animal;

[Serializable]
public class AnimalDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Race { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public int OwnerId { get; set; }

    public Domain.Entities.User? Owner { get; set; }
}