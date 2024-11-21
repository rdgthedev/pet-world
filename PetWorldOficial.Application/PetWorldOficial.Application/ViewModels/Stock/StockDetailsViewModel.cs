namespace PetWorldOficial.Application.ViewModels.Stock;

public class StockDetailsViewModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Domain.Entities.Product Product { get; set; }
    public int Quantity { get; set; }
}