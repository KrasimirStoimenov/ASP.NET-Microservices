namespace AspnetRunBasics.Services;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using AspnetRunBasics.Services.Interfaces;

public class OrderService : IOrderService
{
    private readonly HttpClient client;

    public OrderService(HttpClient client)
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUsernameAsync(string username)
    {
        var response = await this.client.GetAsync($"/Order/{username}");
        return await response.ReadContentAs<IEnumerable<OrderResponseModel>>();
    }
}
