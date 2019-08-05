using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProductCatalog.Core.Dtos
{
    public class AddProductDto
    {
        [Required]
        public int ProductCatalogId{ get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal CurrentPrice { get; set; }

    }
}
