using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Core.Dtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal CurrentPrice { get; set; }

        public IEnumerable<PriceHistoryDto> PriceHistory { get; set; }
    }

    public class PriceHistoryDto
    {
        public DateTime PriceChangeDate { get; set; }

        public decimal Price { get; set; }
    }
}
