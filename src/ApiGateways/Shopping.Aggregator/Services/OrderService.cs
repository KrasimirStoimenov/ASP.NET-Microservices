namespace Shopping.Aggregator.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services.Interfaces;

public class OrderService : IOrderService
{
    private readonly HttpClient client;

    public OrderService(HttpClient client)
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUsername(string username)
    {
        var response = await this.client.GetAsync($"/api/v1/Order/{username}");
        return await response.ReadContentAs<IEnumerable<OrderResponseModel>>();
    }
}
