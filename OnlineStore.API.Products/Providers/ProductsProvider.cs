using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
                    Name = "Bat",
                    Price = 100,
                    Inventory = 127
                }));
                _dbContext.Products.Add((new Db.Product()
                {
                    Id = 2,
                    Name = "Ball",
                    Price = 120,
                    Inventory = 200
                }));
                _dbContext.Products.Add((new Db.Product()
                {
                    Id = 3,
                    Name = "Mat",
                    Price = 300,
                    Inventory = 400
                }));
                _dbContext.Products.Add((new Db.Product()
                {
                    Id = 4,
                    Name = "Badminton",
                    Price = 20,
                    Inventory = 500
                }));
                _dbContext.Products.Add((new Db.Product()
                {
                    Id = 5,
                    Name = "Racket",
                    Price = 175,
                    Inventory = 550
                }));
                _dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await _dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
          
        }

        public async Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                
                if (product != null )
                {
                    var result = _mapper.Map<Db.Product, Models.Product>(product);
                    return (true, result, null);
                }

                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
