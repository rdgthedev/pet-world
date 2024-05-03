namespace PetWorldOficial.Domain.ValueObjects;

public class Address
{
    public Address(string street, int number, string neighborhood, string complement, string city, string state)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        Complement = complement;
        City = city;
        State = state;
    }

    public string Street { get; private set; }
    public int Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string Complement { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
}