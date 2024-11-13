using PetWorldOficial.Domain.Common;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.Entities;

public class Supplier : Entity
{
    public Supplier()
    {
    }

    public Supplier(
        string name,
        string email,
        string cnpj,
        string cellPhone,
        string street,
        int number,
        string neighborhood,
        string? complement,
        string postalCode,
        string city,
        string state)
    {
        Name = name;
        Email = email;
        CNPJ = cnpj;
        CellPhone = cellPhone;
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        Complement = complement;
        PostalCode = postalCode;
        City = city;
        State = state;
        CreatedAt = DateTime.Now;
        Status = EActivingStatus.Active;
        Products = new();
    }

    public string Name { get; private set; }
    public string Email { get; set; }
    public string CNPJ { get; private set; }
    public string CellPhone { get; private set; }
    public string Street { get; private set; }
    public int Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string? Complement { get; private set; }
    public string PostalCode { get; set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public EActivingStatus Status { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastUpdatedAt { get; private set; }
    public List<Product> Products { get; private set; }
}