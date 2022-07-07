namespace AspnetRunBasics.Models;

using System.Collections.Generic;

public class BasketModel
{
    public string Username { get; set; }

    public List<BsketItemModel> ShoppingCartItems { get; set; } = new List<BsketItemModel>();

    public decimal TotalPrice { get; set; }
}
