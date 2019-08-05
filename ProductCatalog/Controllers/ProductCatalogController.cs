using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Dtos;
using ProductCatalog.Core.Services;

namespace ProductCatalog.Api.Controllers
{
    [Route("api/ProductCatalog")]
    public class ProductCatalogController : Controller
    {
        private readonly IProductCatalogService _productCatalogService;

        public ProductCatalogController(IProductCatalogService productCatalogService)
        {
            _productCatalogService = productCatalogService;
        }

        [HttpGet]
        public IActionResult GetProducts(int id)
        {
            var products = _productCatalogService.GetProducts(id);

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }
        
        [HttpPost]
        public IActionResult Addproduct([FromBody]IEnumerable<AddProductDto> productDto)
        {
            try
            {
                var product = _productCatalogService.AddProducts(productDto);

                return Created("", product);
            }
            catch(DbUpdateException e)
            {
                return BadRequest(e);
            }           
        }

        [HttpPatch]
        public IActionResult UpdateProduct(int id, [FromBody]IEnumerable<UpdateProductDto> productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }

            var existingProducts = _productCatalogService.GetProducts(id);

            if (existingProducts == null)
            {
                return NotFound();
            }

            _productCatalogService.UpdateProducts(id, productDto);

            return NoContent();
        }
    }
}
