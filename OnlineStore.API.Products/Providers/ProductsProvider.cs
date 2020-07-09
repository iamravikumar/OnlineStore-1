using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using OnlineStore.API.Products.Db;
using OnlineStore.API.Products.Interfaces;
using Product = OnlineStore.API.Products.Models.Product;

namespace OnlineStore.API.Products.Providers
{
    public class ProductsProvider: IProductsProvider
    {
        private readonly ProductDbContext _dbContext;
        private readonly ILogger<ProductsProvider> _logger;
        private readonly IMapper _mapper;

        public ProductsProvider(ProductDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Products.Any())
            {
                _dbContext.Products.Add((new Db.Product()
                {
                    Id =  1,
                    Name = "Monitor",
                    Price = 100,
                    Inventory = 127
                }));
                _dbContext.Products.Add((new Db.Product()
                {
                    Id = 2,
                    Name = "Keyboard",
                    Price = 120,
                    Inventory = 200
                }));
                _dbContext.Products.Add((new Db.Product()
                {
                    Id = 3,
                    Name = "CPU",
                    Price = 300,
                    Inventory = 400
                }));
                _dbContext.Products.Add((new Db.Product()
                {
                    Id = 4,
                    Name = "Mouse",
                    Price = 20,
                    Inventory = 500
                }));
                _dbContext.Products.Add((new Db.Product()
                {
                    Id = 5,
                    Name = "Chipset",
                    Price = 175,
                    Inventory = 550
                }));
                _dbContext.SaveChanges();
            }
        }

        public Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
