using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using MediatR;
using PetWorldOficial.Application.Validations;

namespace PetWorldOficial.Application.Commands.User;

public record RegisterUserCommand(
    [Required(ErrorMessage = "O nome é obrigatório!")]
    string Name,
    [Required(ErrorMessage = "O genêro é obrigatório!")]
    string Gender,
    [Required(ErrorMessage = "A data de nascimento é obrigatório!")]
    DateTime? BirthDate,
    [Required(ErrorMessage = "O CPF é obrigatório!")]
    [CustomValidation(typeof(CPFValidator), nameof(CPFValidator.IsValid))]
    string Document,
    [Required(ErrorMessage = "O perfil é obrigatório!")]
    string? Role,
    [Required(ErrorMessage = "A senha é obrigatória!")]
    string Password,
    [Required(ErrorMessage = "O email é obrigatório!")]
    string Email,
    [Required(ErrorMessage = "O telefone é obrigatório!")]
    string PhoneNumber,
    [Required(ErrorMessage = "A rua é obrigatória!")]
    string Street,
    [Required(ErrorMessage = "O número é obrigatório!")]
    [Range(1, int.MaxValue, ErrorMessage = "Esse número é inváliado!")]
    int? Number,
    [Required(ErrorMessage = "O CEP é obrigatório!")]
    string PostalCode,
    [Required(ErrorMessage = "O bairro é obrigatório!")]
    string Neighborhood,
    string? Complement,
    [Required(ErrorMessage = "A cidade é obrigatória!")]
    string City,
    [Required(ErrorMessage = "O estado é obrigatório!")]
    string State) : IRequest<Unit>
{
    [Required(ErrorMessage = "A confirmação de senha é obrigatória!")]
    [Compare(nameof(Password), ErrorMessage = "As senhas não coincidem.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}