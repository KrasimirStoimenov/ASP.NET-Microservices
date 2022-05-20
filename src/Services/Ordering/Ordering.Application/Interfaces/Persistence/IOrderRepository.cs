namespace Ordering.Application.Interfaces.Persistence;

using Ordering.Domain.Entities;

public interface IOrderRepository : IAsyncRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByUsernameAsync(string username);
}
