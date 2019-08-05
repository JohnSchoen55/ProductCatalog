using ProductCatalog.Data.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProductCatalog.Data.Repositories
{
    public interface IProductCatalogRepository
    {
        IEnumerable<Products> GetProducts(int productCatalogId);
        IEnumerable<Products> AddProduct(IEnumerable<Products> addProducts);
        IEnumerable<Products> UpdateProducts(IEnumerable<Products> updateProducts);
    }
    public class ProductCatalogRepository : Repository<ProductCatalogContext, Products>, IProductCatalogRepository
    {
        private readonly ProductCatalogContext _context;

        public ProductCatalogRepository(ProductCatalogContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Products> GetProducts(int productCatalogId)
        {
            return this.Include(x => x.PriceHistory)
                .SearchFor(x => x.ProductCatalogId == productCatalogId)
                .ToList();
        }

        public IEnumerable<Products> AddProduct(IEnumerable<Products> addProducts)
        {
            this.InsertRange(addProducts);
            return addProducts;
        }

        public IEnumerable<Products> UpdateProducts(IEnumerable<Products> updateProducts)
        {
            this.UpdateRange(updateProducts);
            return updateProducts;
        }
    }
}
