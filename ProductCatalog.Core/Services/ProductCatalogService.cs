using AutoMapper;
using ProductCatalog.Core.Dtos;
using ProductCatalog.Core.ExtensionMethods;
using ProductCatalog.Data.Entities;
using ProductCatalog.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductCatalog.Core.Services
{
    public interface IProductCatalogService
    {
        IEnumerable<ProductDto> GetProducts(int productCatalogId);

        IEnumerable<ProductDto> AddProducts(IEnumerable<AddProductDto> productDto);

        IEnumerable<Products> UpdateProducts(int productCatalogId, IEnumerable<UpdateProductDto> productDto);
    }

    public class ProductCatalogService : IProductCatalogService
    {
        private readonly IProductCatalogRepository _productCatalogRepository;

        public ProductCatalogService(IProductCatalogRepository productCatalogRepository)
        {
            _productCatalogRepository = productCatalogRepository;
        }

        public IEnumerable<ProductDto> GetProducts(int productCatalogId)
        {
            var products = _productCatalogRepository.GetProducts(productCatalogId);

            if (!products.Any())
            {
                return null;
            }

            return Mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public IEnumerable<ProductDto> AddProducts(IEnumerable<AddProductDto> productDto)
        {
            var products = Mapper.Map<IEnumerable<Products>>(productDto);

            var addedProduct = _productCatalogRepository.AddProduct(products);

            foreach (var product in addedProduct)
            {
                product.PriceHistory = new List<PriceHistory>()
                {
                    new PriceHistory
                    {
                        Price = product.CurrentPrice,
                        ProductName = product.ProductName,
                        PriceChangeDate = DateTime.Now.ToCentralStandardTime()
                    }
                };
            }
                
            var updatedProducts = _productCatalogRepository.UpdateProducts(addedProduct);

            return Mapper.Map<IEnumerable<ProductDto>>(updatedProducts);
        }

        public IEnumerable<Products> UpdateProducts(int productCatalogId, IEnumerable<UpdateProductDto> updateProductDto)
        {
            var existingProducts = _productCatalogRepository.GetProducts(productCatalogId);

            foreach (var product in updateProductDto)
            {
                var existingProduct = existingProducts.FirstOrDefault(x => x.ProductId == product.ProductId);

                if (existingProduct != null)
                {
                    if (existingProduct.CurrentPrice != product.CurrentPrice)
                    {
                        existingProduct.PriceHistory = new List<PriceHistory>
                        {
                            new PriceHistory
                            {
                                Price = product.CurrentPrice,
                                ProductName = product.ProductName,
                                PriceChangeDate = DateTime.Now.ToCentralStandardTime()
                            }
                        };
                    }

                    existingProduct.ProductName = product.ProductName;
                    existingProduct.CurrentPrice = product.CurrentPrice;                   
                }
            }

            return _productCatalogRepository.UpdateProducts(existingProducts);
        }
    }
}