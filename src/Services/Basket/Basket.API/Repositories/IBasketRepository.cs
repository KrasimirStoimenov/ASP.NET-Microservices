namespace Basket.API.Repositories;

using Basket.API.Models;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string username);

    Task<ShoppingCart> UpdateBasket(ShoppingCart basket);

    Task DeleteBasket(string username);
}
