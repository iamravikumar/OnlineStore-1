using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineStore.API.Orders.Db;
using OnlineStore.API.Orders.Profiles;
using OnlineStore.API.Orders.Providers;

namespace OnlineStore.API.Orders.Tests
{
    [TestClass]
    public class OrdersServiceTest
    {
        [TestMethod]
        public async Task GetAllOrdersAsyncTest()
        {
            var options = new DbContextOptionsBuilder<OrdersDbContext>()
                .UseInMemoryDatabase(nameof(GetAllOrdersAsyncTest))
                .Options;
            var dbContext = new OrdersDbContext(options);
            CreateOrders(dbContext);

            var orderProfile = new OrderProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(orderProfile));
            var mapper = new Mapper(configuration);

            var ordersProvider = new OrdersProvider(dbContext, null, mapper);
            var orders = await ordersProvider.GetOrdersAsync(1);
            Assert.IsNotNull(orders.Orders);
            Assert.IsTrue(orders.IsSuccess);
            Assert.IsNull(orders.ErrorMessage);
        }

        private void CreateOrders(OrdersDbContext dbContext)
        {
            for (int i = 21; i <= 30; i++)
            {
                dbContext.Orders.Add(new Order()
                {
                    Id = i,
                    CustomerId = i,
                    Items = new List<OrderItem>(),
                    OrderDate = DateTime.Now,
                    Total = 7*7+i
                });
            }
        }
    }
}
