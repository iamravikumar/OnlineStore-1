using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineStore.API.Orders.Models;

namespace OnlineStore.API.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
