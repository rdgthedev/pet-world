using Microsoft.AspNetCore.Identity;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.Entities;

public sealed class User : IdentityUser<int>
{
    public User(
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
        string state) : base (userName)
    {
        Name = name;
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

    public string Name { get; set; } 
    public string Gender { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string Document { get; private set; }
    public string Street { get; private set; }
    public int Number { get; private set; }
    public string PostalCode { get; private set; }
    public string Neighborhood { get; private set; }
    public string Complement { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public List<Animal> Animals { get; private set; }
}