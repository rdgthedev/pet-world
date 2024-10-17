using Microsoft.VisualBasic;

namespace PetWorldOficial.Application.ViewModels.Animal;

[Serializable]
public class AnimalDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public Domain.Entities.Category Category { get; set; } = null!;
    public Domain.Entities.Race Race { get; set; } = null!;
    public string Gender { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public int OwnerId { get; set; }

    public Domain.Entities.User? Owner { get; set; }
}