namespace PetWorldOficial.Application.DTOs.Animal.Input;

public class RegisterAnimalDTO
{
    public string Name { get; set; }
    public string Species { get; set; }
    public string Race { get; set; }
    public DateTime? BirthDate { get; set; }
    public string Gender { get; set; }
    public int UserId { get; set; }
}