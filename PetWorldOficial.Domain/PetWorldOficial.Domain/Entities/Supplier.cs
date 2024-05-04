using PetWorldOficial.Domain.Common;

namespace PetWorldOficial.Domain.Entities;

public class Supplier : Entity
{
    public Supplier(
        string company, 
        string cnpj, 
        string representative, 
        string street, 
        int number, 
        string neighborhood, 
        string complement, 
        string city, 
        string state)
    {
        Company = company;
        CNPJ = cnpj;
        Representative = representative;
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        Complement = complement;
        City = city;
        State = state;
    }

    public string Company { get; private set; }
    public string CNPJ { get; private set; }
    public string Representative { get; private set; }
    public string Street { get; private set; }
    public int Number { get; private set; }
    public string Neighborhood { get; private set; }
    public string Complement { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }

    public void Update(
        string company, 
        string cnpj, 
        string representative, 
        string street, 
        int number, 
        string neighborhood, 
        string complement, 
        string city, 
        string state)
    {
        Company = company;
        CNPJ = cnpj;
        Representative = representative;
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        Complement = complement;
        City = city;
        State = state;
    }
}