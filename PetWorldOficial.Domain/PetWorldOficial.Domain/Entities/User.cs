using Microsoft.AspNetCore.Identity;
using PetWorldOficial.Domain.Common;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.Entities;

public sealed class User : IdentityUser<int>
{
    public User(
        string name,
        string userName,
        EGender gender,
        DateTime birthDate,
        string document,
        string email,
        string phoneNumber,
        string street,
        int number,
        string postalCode,
        string neighborhood,
        string? complement,
        string city,
        string state) : base(userName)
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
        CreatedAt = DateTime.Now;
        Animals = new();
        Schedullings = new();
    }

    public string Name { get; set; }
    public EGender Gender { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string Document { get; private set; }
    public string Street { get; private set; }
    public int Number { get; private set; }
    public string PostalCode { get; private set; }
    public string Neighborhood { get; private set; }
    public string? Complement { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime LastUpdatedAt { get; private set; }
    public List<Animal> Animals { get; private set; }
    public List<Schedulling> Schedullings { get; set; }

    public void Update(
        string name,
        string userName,
        EGender gender,
        DateTime birthDate,
        string document,
        string email,
        string phoneNumber,
        string street,
        int number,
        string postalCode,
        string neighborhood,
        string? complement,
        string city,
        string state)
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

        LastUpdatedAt = DateTime.Now;
    }
}