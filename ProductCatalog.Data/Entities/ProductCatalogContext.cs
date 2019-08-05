using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data.DBContext;
using System;
using System.Collections.Generic;

namespace ProductCatalog.Data.Entities
{
    public class ProductCatalogContext : DbContext, IBasicContext
    {
        public ProductCatalogContext(DbContextOptions<ProductCatalogContext> options)
            : base(options)
        { }

        public DbSet<ProductCatalog> ProductCatalog { get; set; }

        public DbSet<Products> Products { get; set; }

        public DbSet<PriceHistory> PriceHistory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductCatalog>().HasData(new ProductCatalog { ProductCatalogId = 1, Name = "Stuff" });
        }
    }    
}