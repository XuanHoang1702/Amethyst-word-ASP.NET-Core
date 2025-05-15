using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.DTO
{
    public class ProductUpdateDTO
    {
        public int PRODUCT_ID { get; set; }
        public string PRODUCT_NAME { get; set; }
        public decimal PRODUCT_PRICE { get; set; }
        public string PRODUCT_DETAIL { get; set; }
        public string PRODUCT_DESCRIPTION { get; set; }
        public int BRAND_ID { get; set; }
        public int CATEGORY_ID { get; set; }
        public string PRODUCT_STATUS { get; set; }  
    }
}
