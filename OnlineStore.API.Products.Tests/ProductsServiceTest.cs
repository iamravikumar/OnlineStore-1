using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineStore.API.Products.Db;
using OnlineStore.API.Products.Profiles;
using OnlineStore.API.Products.Providers;

namespace OnlineStore.API.Products.Tests
{
    [TestClass]
    public class ProductsServiceTest
    {
        [TestMethod]
        public async Task GetAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetAllProducts))
                .Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductsAsync();
            Assert.IsTrue(product.IsSuccess);
            Assert.IsTrue(product.Products.Any());
            Assert.IsNull(product.ErrorMessage);
        }

        [TestMethod]
        public async Task GetAllProductById()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetAllProductById))
                .Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync(1);
            Assert.IsTrue(product.IsSuccess);
            Assert.IsNotNull(product.Product);
            Assert.IsNull(product.ErrorMessage);
        }

        [TestMethod]
        public async Task GetAllProductByInvalidId()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetAllProductByInvalidId))
                .Options;
            var dbContext = new ProductDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductAsync(11);
            Assert.IsFalse(product.IsSuccess);
            Assert.IsNull(product.Product);
            Assert.IsNotNull(product.ErrorMessage);
        }

        private void CreateProducts(ProductDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)
                });
            }
            dbContext.SaveChanges();
        }
    }
}
