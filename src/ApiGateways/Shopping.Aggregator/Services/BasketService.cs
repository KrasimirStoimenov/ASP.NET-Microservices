namespace Shopping.Aggregator.Services;

using System.Threading.Tasks;

using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services.Interfaces;

public class BasketService : IBasketService
{
    private readonly HttpClient client;

    public BasketService(HttpClient client)
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<BasketModel> GetBasket(string username)
    {
        var response = await this.client.GetAsync($"/api/v1/Basket/{username}");
        return await response.ReadContentAs<BasketModel>();
    }
}
