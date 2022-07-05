namespace Shopping.Aggregator.Services;

using System.Threading.Tasks;

using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services.Interfaces;

public class BasketService : IBasketService
{
    private readonly HttpClient client;

    public BasketService(HttpClient client)
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public Task<BasketModel> GetBasket(string username)
    {
        throw new NotImplementedException();
    }
}
