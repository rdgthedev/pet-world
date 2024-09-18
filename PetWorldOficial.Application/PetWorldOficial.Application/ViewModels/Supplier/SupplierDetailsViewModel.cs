namespace PetWorldOficial.Application.ViewModels.Supplier;

public record SupplierDetailsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string CellPhone { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}