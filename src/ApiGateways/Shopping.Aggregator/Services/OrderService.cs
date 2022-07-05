namespace Shopping.Aggregator.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services.Interfaces;

public class OrderService : IOrderService
{
    private readonly HttpClient client;

    public OrderService(HttpClient client)
    {
        this.client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string username)
    {
        throw new NotImplementedException();
    }
}
