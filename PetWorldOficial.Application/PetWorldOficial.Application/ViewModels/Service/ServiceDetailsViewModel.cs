namespace PetWorldOficial.Application.ViewModels.Service;

public record ServiceDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public string ImageUrl { get; set; }
}