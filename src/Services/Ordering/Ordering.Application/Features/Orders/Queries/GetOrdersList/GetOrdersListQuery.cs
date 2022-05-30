namespace Ordering.Application.Features.Orders.Queries.GetOrdersList;

using MediatR;

public class GetOrdersListQuery : IRequest<ICollection<OrderModel>>
{
    public GetOrdersListQuery(string username)
    {
        this.Username = username ?? throw new ArgumentNullException(nameof(username));
    }

    public string Username { get; set; }
}
