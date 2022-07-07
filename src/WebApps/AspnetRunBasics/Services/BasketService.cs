namespace AspnetRunBasics.Services;

using System;
using System.Net.Http;
using System.Threading.Tasks;

using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Interfaces;

public class BasketService : IBasketService
{
    private readonly HttpClient client;

    public BasketService(HttpClient client)
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<BasketModel> GetBasketAsync(string username)
    {
        var response = await this.client.GetAsync($"/Basket/{username}");
        return await response.ReadContentAs<BasketModel>();
    }

    public async Task<BasketModel> UpdateBasketAsync(BasketModel model)
    {
        var response = await this.client.PostAsJson("/Basket", model);
        if (response.IsSuccessStatusCode)
        {
            return await response.ReadContentAs<BasketModel>();
        }
        else
        {
            throw new Exception("Something went wrong when calling api.");
        }
    }

    public async Task CheckoutBasketAsync(BasketCheckoutModel model)
    {
        var response = await this.client.PostAsJson("/Basket/Checkout", model);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Something went wrong when calling api.");
        }
    }
}
