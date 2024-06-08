using System.Runtime.InteropServices.JavaScript;

namespace PetWorldOficial.Application.DTOs.Animal.Input;

public record RegisterAnimalDTO(
    string Name,
    string Species,
    string Race,
    string Gender,
    int UserId);