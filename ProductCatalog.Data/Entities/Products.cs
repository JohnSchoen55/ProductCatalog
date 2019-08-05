using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductCatalog.Data.Entities
{
    public class Products
    {
        public Products()
        {
            PriceHistory = new HashSet<PriceHistory>();
        }

        [Key]
        public int ProductId { get; private set; }

        [ForeignKey("ProductCatalog")]
        public int ProductCatalogId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Column(TypeName = "Money")]
        public decimal CurrentPrice { get; set; }

        public ICollection<PriceHistory> PriceHistory { get; set; }
    }
}