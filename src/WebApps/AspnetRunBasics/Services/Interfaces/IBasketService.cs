namespace AspnetRunBasics.Services.Interfaces;

using System.Threading.Tasks;

using AspnetRunBasics.Models;

public interface IBasketService
{
    Task<BasketModel> GetBasketAsync(string username);

    Task<BasketModel> UpdateBasketAsync(BasketModel model);

    Task CheckoutBasketAsync(BasketCheckoutModel model);
}
