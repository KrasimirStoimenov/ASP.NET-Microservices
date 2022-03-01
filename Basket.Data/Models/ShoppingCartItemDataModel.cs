namespace Basket.Data.Models;
public class ShoppingCartItemDataModel
{
    public int Quantity { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
}
