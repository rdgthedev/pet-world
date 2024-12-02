using MediatR;

namespace PetWorldOficial.Application.Commands.Supplier;

public class DeleteSupplierCommand : IRequest<DeleteSupplierCommand>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string CNPJ { get; set; }
    public string CellPhone { get; set; }
    public string Street { get; set; }
    public int Number { get; set; }
    public string Neighborhood { get; set; }
    public string? Complement { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string? Message { get; set; }
}