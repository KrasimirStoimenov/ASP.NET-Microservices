namespace Ordering.Infrastructure.Persistence;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using Ordering.Domain.Common;
using Ordering.Domain.Entities;

public class OrderContext : DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = "ks";
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedDate = DateTime.Now;
                    entry.Entity.LastModifiedBy = "ks";
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
