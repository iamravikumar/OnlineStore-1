using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineStore.API.Orders.Db;
using OnlineStore.API.Orders.Interfaces;
using Order = OnlineStore.API.Orders.Models.Order;

namespace OnlineStore.API.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext _dbContext;
        private readonly ILogger<OrdersProvider> _logger;
        private readonly IMapper _mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Orders.Any())
            {
                _dbContext.Orders.Add(new Db.Order()
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 7, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 6, UnitPrice = 20 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 5, UnitPrice = 30 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 4, UnitPrice = 40 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 3, UnitPrice = 100 }
                    },
                    Total = 700
                });
                _dbContext.Orders.Add(new Db.Order()
                {
                    Id = 2,
                    CustomerId = 1,
                    OrderDate = DateTime.Now.AddDays(-1),
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 7, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 6, UnitPrice = 20 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 5, UnitPrice = 30 },
                        new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 4, UnitPrice = 40 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 3, UnitPrice = 100 }
                    },
                    Total = 700
                });
                _dbContext.Orders.Add(new Db.Order()
                {
                    Id = 3,
                    CustomerId = 2,
                    OrderDate = DateTime.Now,
                    Items = new List<OrderItem>()
                    {
                        new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 7, UnitPrice = 10 },
                        new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 6, UnitPrice = 20 },
                        new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 5, UnitPrice = 30 },
                        new OrderItem() { OrderId = 3, ProductId = 3, Quantity = 3, UnitPrice = 100 }
                    },
                    Total = 700
                });
                _dbContext.SaveChanges();
            }
        }

     
        public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await _dbContext.Orders
                    .Where(o => o.CustomerId == customerId)
                    .Include(o => o.Items)
                    .ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }

                return (false, null, "Order not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
