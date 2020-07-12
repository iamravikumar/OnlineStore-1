using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineStore.API.Customers.Db;
using OnlineStore.API.Customers.Profiles;
using OnlineStore.API.Customers.Providers;

namespace OnlineStore.API.Customers.Tests
{
    [TestClass]
    public class CustomersServiceTest
    {
        [TestMethod]
        public async Task GetCustomersAsyncTest()
        {
            var options = new DbContextOptionsBuilder<CustomersDbContext>()
                .UseInMemoryDatabase(nameof(GetCustomersAsyncTest))
                .EnableSensitiveDataLogging()
                .Options;

             var dbContext = new CustomersDbContext(options);
             CreateCustomers(dbContext);

             var customerProfile = new CustomerProfile();
             var configuration = new MapperConfiguration(config => config.AddProfile(customerProfile));
             var mapper = new Mapper(configuration);

             var customersProvider = new CustomersProvider(dbContext, null, mapper);
            
             var customers = await customersProvider.GetCustomersAsync();
             Assert.IsTrue(customers.Customers.Any());
             Assert.IsTrue(customers.IsSuccess);
             Assert.IsNull(customers.ErrorMessage);

        }

        [TestMethod]
        public async Task GetCustomerAsyncTest()
        {
            var options = new DbContextOptionsBuilder<CustomersDbContext>()
                .UseInMemoryDatabase(nameof(GetCustomerAsyncTest))
                .EnableSensitiveDataLogging()
                .Options;

            var dbContext = new CustomersDbContext(options);
            CreateCustomers(dbContext);

            var customerProfile = new CustomerProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(customerProfile));
            var mapper = new Mapper(configuration);

            var customersProvider = new CustomersProvider(dbContext, null, mapper);

            var customers = await customersProvider.GetCustomerAsync(11);
            Assert.IsNotNull(customers.Customer);
            Assert.IsTrue(customers.IsSuccess);
            Assert.IsNull(customers.ErrorMessage);

        }

        private void CreateCustomers(CustomersDbContext dbContext)
        {
            for (int i = 11; i < 20; i++)
            {
                dbContext.Customers.Add(new Customer()
                {
                    Id = i,
                    Address = $"Address of {i} Customer",
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString(),
                    Zipcode = "834002"
                });
            }
         
            dbContext.SaveChanges();
        }
    }
}
