using MediatR;

namespace PetWorldOficial.Application.Commands.Supplier;

public class DeleteSupplierCommand : IRequest<DeleteSupplierCommand>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CNPJ { get; set; } = string.Empty;
    public string CellPhone { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public int Number { get; set; }
    public string Neighborhood { get; set; } = string.Empty;
    public string? Complement { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string? Message { get; set; } = string.Empty;
}