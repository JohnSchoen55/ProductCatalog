using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductCatalog.Data.Entities
{
    public class PriceHistory
    {
        [Key]
        public int PriceHistoryId { get; set; }

        [ForeignKey("Products")]
        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Required]
        public DateTime PriceChangeDate { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

    }
}
