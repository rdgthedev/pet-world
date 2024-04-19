using Flunt.Notifications;
using Flunt.Validations;

namespace PetWorldOficial.Domain.ValueObjects;

public class Address : Notifiable<Notification>
{
    public Address(string street, int number, string neighborhood, string complement, string city, string state)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        Complement = complement;
        City = city;
        State = state;
        
        AddNotifications(
            new Contract<Address>()
            .Requires()
            .IsGreaterThan(
                Street.Length, 
                2, 
                "Address.Street", 
                "O campo rua deve conter pelo menos 3 caracteres")
            .IsNotNullOrEmpty(
                Street, 
                "Address.Street", 
                "O campo rua não pode ser vázio!")
            .IsGreaterThan(
                Number, 
                0, 
                "Address.Number", 
                "O campo número deve maior que 0")
            .IsGreaterThan(
                Neighborhood.Length,
                2, 
                "Address.Neighborhood",
                "O campo bairro deve conter pelo menos 3 caracteres")
            .IsNotNullOrEmpty(
                Neighborhood,
                "",
                "O campo bairro não pode ser vázio!")
            .IsNotNullOrEmpty(
                City,
                "Address.City",
                "O campo cidade não pode ser vázio!")
            .IsGreaterThan(
                City.Length,
                2, 
                "Address.City",
                "O campo cidade deve conter pelo menos 3 caracteres")
            .IsNotNullOrEmpty(
                State,
                "Address.State",
                "O campo estado não pode ser vázio!")
            .IsGreaterThan(
                State.Length,
                1, 
                "Address.City",
                "O campo estado deve conter pelo menos 2 caracteres"));
    }

    public string Street { get; private set; }
    public int Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string Complement { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
}