namespace AspnetRunBasics.Services.Interfaces;

using System.Collections.Generic;
using System.Threading.Tasks;

using AspnetRunBasics.Models;

public interface IOrderService
{
    Task<IEnumerable<OrderResponseModel>> GetOrdersByUsernameAsync(string username);
}
