using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.API.Search.Interfaces
{
   public interface ISearchService
    {
        Task<(bool IsSuccess, dynamic SearchResults )> SearchAsync(int CustomerId);
    }
}
