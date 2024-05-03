using PetWorldOficial.Domain.Enums;

namespace PetWorldOficial.Domain.ValueObjects;

public class Document
{
    public Document(string number)
    {
        Number = number;
        Type = Validation(number);
    }

    public EDocument? Type { get; private set; }
    public string Number { get; private set; }

    private EDocument? Validation(string number)
    {
        return number.Length switch
        {
            8 => EDocument.Cpf,
            11 => EDocument.Cnpj,
            _ => null
        };
    }
}