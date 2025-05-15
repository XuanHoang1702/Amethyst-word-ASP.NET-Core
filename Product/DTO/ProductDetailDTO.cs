using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.DTO
{
    public class ProductVariantDTO
    {
        public string? COLOR_NAME { get; set; }
        public string? SIZE_NAME { get; set; }
        public int? QUANTITY_COLOR { get; set; }
        public int? QUANTITY_SIZE { get; set; }
    }

    public class ProductDetailDTO
    {
        public int PRODUCT_ID { get; set; }
        public string PRODUCT_NAME { get; set; }
        public string? IMAGE_NAME { get; set; }
        public decimal PRODUCT_PRICE { get; set; }
        public string PRODUCT_DETAIL { get; set; }
        public string PRODUCT_DESCRIPTION { get; set; }
        public decimal RATE { get; set; }
        public string? COLOR_NAME { get; set; }
        public string? SIZE_NAME { get; set; }
        public int? QUANTITY_COLOR { get; set; }
        public int? QUANTITY_SIZE { get; set; }
        public int? QUANTITY_TOTAL { get; set; }
        public string PRODUCT_STATUS { get; set; }
        public DateTime CREATED_AT { get; set; }
        public DateTime UPDATED_AT { get; set; }

        public List<ProductVariantDTO> Variants { get; set; } = new();
    }

}
