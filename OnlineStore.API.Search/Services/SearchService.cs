using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStore.API.Search.Interfaces;

namespace OnlineStore.API.Search.Services
{
    public class SearchService : ISearchService
    {
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int CustomerId)
        {
            await Task.Delay(1);
            return (true, new {Message = "Hello"});
        }
    }
}
