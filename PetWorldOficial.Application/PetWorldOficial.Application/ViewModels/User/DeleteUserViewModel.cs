namespace PetWorldOficial.Application.ViewModels.User;

public class DeleteUserViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}