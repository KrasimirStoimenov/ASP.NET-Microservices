namespace Ordering.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

using Ordering.Application.Interfaces.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(OrderContext dbContext)
        : base(dbContext)
    { }

    public async Task<IEnumerable<Order>> GetOrdersByUsernameAsync(string username)
        => await this.dbContext.Orders
            .Where(o => o.Username == username)
            .ToListAsync();
}
