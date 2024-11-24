namespace PetWorldOficial.Application.ViewModels.User;

public class UserDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string? RoleName { get; set; }
    public string Gender { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string Document { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string SecurityStamp { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int? Number { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string Complement { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ConcurrencyStamp { get; set; } = string.Empty;
}