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

        public SearchService(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int CustomerId)
        {
            //await Task.Delay(1);
            //return (true, new {Message = "Hello"});
            var ordersResult = await _ordersService.GetOrdersAsync(CustomerId);
            if (ordersResult.IsSuccess)
            {
                var result = new
                {
                    Orders = ordersResult.Orders
                };
                return (true, result);
            }

            return (false, null);
        }
    }
}
