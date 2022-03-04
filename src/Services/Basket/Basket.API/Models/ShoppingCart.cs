namespace Basket.API.Models;
public class ShoppingCart
{
    public ShoppingCart()
    {
    }

    public ShoppingCart(string username)
    {
        Username = username;
    }

    public string Username { get; set; }

    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

    public decimal TotalPrice
    {
        get
        {
            decimal totalprice = 0;
            foreach (var item in ShoppingCartItems)
            {
                totalprice += item.Price * item.Quantity;
            }
            return totalprice;
        }
    }
}
