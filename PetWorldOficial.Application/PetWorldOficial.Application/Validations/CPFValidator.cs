using System.ComponentModel.DataAnnotations;

namespace PetWorldOficial.Application.Validations;

public static class CPFValidator
{
    public static ValidationResult? IsValid(string cpf, ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return null!;

        cpf = cpf.Replace(".", "").Replace("-", "");

        if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            return new ValidationResult("CPF inválido. Deve conter 11 números.");
        
        var invalidCpfs = new[]
        {
            "00000000000", "11111111111", "22222222222", "33333333333",
            "44444444444", "55555555555", "66666666666", "77777777777",
            "88888888888", "99999999999"
        };

        if (invalidCpfs.Contains(cpf))
            return new ValidationResult("Este CPF é inválido.");
        
        var digits = cpf.Select(c => int.Parse(c.ToString())).ToArray();

        for (var i = 9; i < 11; i++)
        {
            var sum = digits.Take(i).Select((d, j) => d * (i + 1 - j)).Sum();
            var verifier = (sum * 10) % 11;
            if (verifier == 10) verifier = 0;

            if (digits[i] != verifier)
                return new ValidationResult("Este CPF é inválido.");
        }

        return ValidationResult.Success; // Validação bem-sucedida
    }
}