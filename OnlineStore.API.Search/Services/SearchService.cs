using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStore.API.Search.Interfaces;

namespace OnlineStore.API.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService _ordersService;
        private readonly IProductsService _productsService;
        private readonly ICustomersService _customersService;

        public SearchService(IOrdersService ordersService, IProductsService productsService, ICustomersService customersService)
        {
            _ordersService = ordersService;
            _productsService = productsService;
            _customersService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int CustomerId)
        {
            //await Task.Delay(1);
            //return (true, new {Message = "Hello"});
            var ordersResult = await _ordersService.GetOrdersAsync(CustomerId);
            var productResult = await _productsService.GetProductsAsync();
            var customerResult = await _customersService.GetCustomerAsync(CustomerId);
            if (ordersResult.IsSuccess)
            {
                foreach (var order in ordersResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productResult.IsSuccess ? productResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name
                                            : "Looks like Product Microservice is down";
                    }
                }
                var result = new
                {
                    Customer = customerResult.IsSuccess? customerResult.Customer : new {Name="Customer Microservice is down at the moment"},
                    Orders = ordersResult.Orders
                };
                return (true, result);
            }

            return (false, null);
        }
    }
}
