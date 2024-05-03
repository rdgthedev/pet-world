using Microsoft.AspNetCore.Identity;

namespace PetWorldOficial.Infrastructure.IdentityEntities;

public sealed class ApplicationUser : IdentityUser<int>
{
    public ApplicationUser(
        string name, 
        string userName,
        string gender,
        DateTime birthDate,
        string document,
        string email,
        string phoneNumber,
        string street,
        int number,
        string postalCode,
        string neighborhood,
        string complement,
        string city,
        string state)
    {
        Name = name;
        UserName = userName;
        Gender = gender;
        BirthDate = birthDate;
        Document = document;
        Email = email;
        PhoneNumber = phoneNumber;
        Street = street;
        Number = number;
        PostalCode = postalCode;
        Neighborhood = neighborhood;
        Complement = complement;
        City = city;
        State = state;
    }

    public string Name { get; private set; }
    public string Document { get; private  set; }
    public string Gender { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string Street { get; private set; }
    public int Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string Complement { get; private set; }
    public string PostalCode { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
}