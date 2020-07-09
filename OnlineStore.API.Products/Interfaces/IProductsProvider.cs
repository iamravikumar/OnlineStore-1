using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineStore.API.Products.Models;


namespace OnlineStore.API.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
    }
}
