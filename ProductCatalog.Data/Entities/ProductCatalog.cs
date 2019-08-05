using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductCatalog.Data.Entities
{
    public class ProductCatalog
    {
        public ProductCatalog()
        {
            Products = new HashSet<Products>();
        }

        [Key]
        public int ProductCatalogId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<Products> Products { get; set; }
    }
}
