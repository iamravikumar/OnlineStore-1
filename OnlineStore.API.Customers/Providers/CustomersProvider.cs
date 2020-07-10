using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineStore.API.Customers.Db;
using OnlineStore.API.Customers.Interfaces;
using Customer = OnlineStore.API.Customers.Models.Customer;

namespace OnlineStore.API.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersProvider> _logger;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            
            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Customers.Any())
            {
                _dbContext.Customers.Add((new Db.Customer()
                {
                    Id = 1,
                    FirstName = "Rahul",
                    LastName = "Sahay",
                    Zipcode = "834002",
                    Address = "Bariyatu, Ranchi"
                }));
                _dbContext.Customers.Add((new Db.Customer()
                {
                    Id = 2,
                    FirstName = "Rohit",
                    LastName = "Sahay",
                    Zipcode = "560001",
                    Address = "RajBhavan Bangalore"
                }));
                _dbContext.Customers.Add((new Db.Customer()
                {
                    Id = 3,
                    FirstName = "Mohit",
                    LastName = "Sahay",
                    Zipcode = "560076",
                    Address = "Bannerghatta Road, Bangalore"
                }));
                _dbContext.Customers.Add((new Db.Customer()
                {
                    Id = 4,
                    FirstName = "Anita",
                    LastName = "Kumari",
                    Zipcode = "834003",
                    Address = "Hatia, Ranchi"
                }));
                _dbContext.Customers.Add((new Db.Customer()
                {
                    Id = 5,
                    FirstName = "Shalini",
                    LastName = "Kumari",
                    Zipcode = "834009",
                    Address = "Hehal, Ranchi"
                }));
                _dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                _logger?.LogInformation("Customer Information");
                var customers = await _dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Customer>>(customers);
                    return (true, result, null);
                }

                return (false, null, "Customers not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, "Customers not found");
            }
        }

        public async Task<(bool IsSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
                if (customer != null)
                {
                    var result = _mapper.Map<Db.Customer, Customer>(customer);
                    return (true, result, null);
                }

                return (false, null, $"Customer with Id:- {id} not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, $"Customer with Id:- {id} not found");
            }
        }
    }
}
