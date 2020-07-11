using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineStore.API.Search.Models;

namespace OnlineStore.API.Search.Interfaces
{
    public interface ICustomersService
    {
        Task<(bool IsSuccess, dynamic Customer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
