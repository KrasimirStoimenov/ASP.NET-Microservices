namespace Ordering.Infrastructure.Persistence;

using Microsoft.Extensions.Logging;

using Ordering.Domain.Entities;

public class OrderContextSeed
{
    public static async Task SeedDataAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetPreconfiguredOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
        }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>
            {
                new Order() {Username = "ks", FirstName = "Krasimir", LastName = "Stoimenov", EmailAddress = "someMail@gmail.com", AddressLine = "Blagoevgrad", Country = "Bulgaria", TotalPrice = 500 }
            };
    }
}
