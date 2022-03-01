namespace Basket.Data.Models;
public class ShoppingCartDataModel
{
    public ShoppingCartDataModel()
    {
    }

    public ShoppingCartDataModel(string username)
    {
        this.Username = username;
    }

    public string Username { get; set; }

    public ICollection<ShoppingCartItemDataModel> ShoppingCartItems { get; set; }


    //TODO: ServiceLayer
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
