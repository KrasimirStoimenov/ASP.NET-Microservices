namespace Shopping.Aggregator.Models;

public class BasketModel
{
    public string Username { get; set; }

    public List<BsketItemExtendedModel> Items { get; set; } = new List<BsketItemExtendedModel>();

    public decimal TotalPrice { get; set; }
}
