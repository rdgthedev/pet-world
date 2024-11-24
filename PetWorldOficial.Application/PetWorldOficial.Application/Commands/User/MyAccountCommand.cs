using System.ComponentModel.DataAnnotations;
using MediatR;
using PetWorldOficial.Application.Validations;
using Stripe.Tax;

namespace PetWorldOficial.Application.Commands.User;

public class MyAccountCommand : IRequest<MyAccountCommand>
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O genêro é obrigatório!")]
    public string Gender { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data de nascimento é obrigatório!")]
    public DateTime? BirthDate { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório!")]
    [CustomValidation(typeof(CPFValidator), nameof(CPFValidator.IsValid))]
    public string Document { get; set; } = string.Empty;

    public string? Password { get; set; }

    [NewPasswordRequiredIfPasswordNotEmpty]
    public string? NewPassword { get; set; }

    [Required(ErrorMessage = "O email é obrigatório!")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O telefone é obrigatório!")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "A rua é obrigatória!")]
    public string Street { get; set; } = string.Empty;

    [Required(ErrorMessage = "O número é obrigatório!")]
    public int? Number { get; set; }

    [Required(ErrorMessage = "O CEP é obrigatório!")]
    public string PostalCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "O bairro é obrigatório!")]
    public string Neighborhood { get; set; } = string.Empty;

    public string? Complement { get; set; }

    [Required(ErrorMessage = "A cidade é obrigatória!")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "O estado é obrigatório!")]
    public string State { get; set; } = string.Empty;

    public string? PasswordHash { get; set; }
    public string? ConcurrencyStamp { get; set; }

    public string? Message { get; set; }
}

public class NewPasswordRequiredIfPasswordNotEmptyAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        var model = (MyAccountCommand)validationContext.ObjectInstance;
        if (!string.IsNullOrEmpty(model.Password) && string.IsNullOrEmpty(model.NewPassword))
        {
            return new ValidationResult("A nova senha é obrigatória quando a senha atual for fornecida.");
        }

        return ValidationResult.Success;
    }
}