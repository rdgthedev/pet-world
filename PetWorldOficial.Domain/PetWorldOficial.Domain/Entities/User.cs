using System.Reflection.Metadata.Ecma335;
using Flunt.Validations;
using PetWorldOficial.Domain.Common;
using PetWorldOficial.Domain.Enums;
using PetWorldOficial.Domain.ValueObjects;

namespace PetWorldOficial.Domain.Entities;

public class User : Entity
{
    public User(
        Name name, 
        string userName, 
        Gender gender, 
        Document document,
        BirthDate birthDate,
        Address address
        )
    {
        Name = name;
        UserName = userName;
        Gender = gender;
        Document = document;
        BirthDate = birthDate;
        Address = address;
        AddNotifications(
            Name,
            Gender, 
            Document,
            BirthDate,
            Address,
            new Contract<User>()
                .Requires()
                .IsNotNullOrEmpty(
                    UserName,
                    "User.UserName",
                    "O campo Nome de Usuário não pode ser vázio"));
    }
    
    public Name Name { get; private set; }
    public string UserName { get; private set; }
    public Document Document { get; private set; }
    public Gender Gender { get; private set; }
    public BirthDate BirthDate { get; private set; }
    public  Address Address { get; private set; }
}