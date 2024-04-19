using Flunt.Notifications;
using Flunt.Validations;
using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.ValueObjects;

public class Document : Notifiable<Notification>
{
    public Document(string number, EDocument type)
    {
        Number = number;
        Type = type;
    }

    public EDocument Type { get; private set; }
    public string Number { get; private set; }

    public bool Validate()
    {
        switch (Number.Length)
        {
            case 8 when Type == EDocument.Cpf:
            case 11 when Type == EDocument.Cnpj:
                return true;
            default:
                AddNotification(new Notification(
                    "Document.Number",
                    "No caso de CPF deve ter 8 digitos e CNPJ 11 digitos (SEM CARACTERES ESPECIAIS) "));
                return false;
        }
    }
}