namespace AspnetRunBasics.Models;

using System.Collections.Generic;

public class BasketModel
{
    public string Username { get; set; }

    public List<BasketItemModel> ShoppingCartItems { get; set; } = new List<BasketItemModel>();

    public decimal TotalPrice { get; set; }
}
