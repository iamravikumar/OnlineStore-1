﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.API.Products.Db
{
    public class ProductDbContext: DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
