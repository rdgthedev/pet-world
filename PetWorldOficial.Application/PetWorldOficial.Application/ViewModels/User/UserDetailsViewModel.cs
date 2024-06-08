namespace PetWorldOficial.Application.ViewModels.User;

public class UserDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string Document { get; set; }
    public string PasswordHash { get; set; }
    public string SecurityStamp { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Street { get; set; }
    public int? Number { get; set; }
    public string PostalCode { get; set; }
    public string Neighborhood { get; set; }
    public string Complement { get; set; }
    public string City { get; set; }
    public string State { get; set; }
}