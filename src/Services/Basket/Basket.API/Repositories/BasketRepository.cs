namespace Basket.API.Repositories;

using System.Threading.Tasks;

using Basket.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache redisCahche;

    public BasketRepository(IDistributedCache redisCahche)
    {
        this.redisCahche = redisCahche ?? throw new ArgumentException(nameof(redisCahche));
    }

    public async Task<ShoppingCart> GetBasket(string username)
    {
        string basket = await this.redisCahche.GetStringAsync(username);

        return JsonConvert.DeserializeObject<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
    {
        await redisCahche.SetStringAsync(basket.Username, JsonConvert.SerializeObject(basket));

        return await GetBasket(basket.Username);
    }

    public async Task DeleteBasket(string username)
    {
        await redisCahche.RemoveAsync(username);
    }
}
