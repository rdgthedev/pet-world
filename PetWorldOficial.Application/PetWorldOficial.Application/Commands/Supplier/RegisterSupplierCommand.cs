using System.ComponentModel.DataAnnotations;
using MediatR;

namespace PetWorldOficial.Application.Commands.Supplier;

public class RegisterSupplierCommand : IRequest<RegisterSupplierCommand>
{
    [Required(ErrorMessage = "O nome é obrigatório!")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "O email é obrigatório!")]
    [EmailAddress(ErrorMessage = "Formato inválido! Utilize algo como: exemplo@dominio.com")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O cnpj é obrigatório!")]
    public string CNPJ { get; set; } = string.Empty;

    [Required(ErrorMessage = "O telefone é obrigatório!")]
    [Phone(ErrorMessage = "O telefone não está no formato correto")]
    public string CellPhone { get; set; } = string.Empty;

    [Required(ErrorMessage = "A rua é obrigatória!")]
    public string Street { get; set; } = string.Empty;

    [Required(ErrorMessage = "O número é obrigatório!")]
    [Range(1, int.MaxValue, ErrorMessage = "O número mínimo é 1.")]
    public int? Number { get; set; }

    [Required(ErrorMessage = "O bairro é obrigatório!")]
    public string Neighborhood { get; set; } = string.Empty;

    public string? Complement { get; set; } = string.Empty;

    [Required(ErrorMessage = "O cep é obrigatório!")]
    public string PostalCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "A cidade é obrigatória!")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "O estado é obrigatório!")]
    public string State { get; set; } = string.Empty;

    public string? Message { get; set; } = string.Empty;
}