using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brand.DTO
{
    public class BrandDTO
    {
        public string? BRAND_ID { get; set; }
        public string? BRAND_NAME { get; set; }
        public string? BRAND_IMAGE { get; set; }
        public string? BRAND_STATUS { get; set; }
        public DateTime CREATED_AT { get; set; }
        public DateTime UPDATE_AT { get; set; }
        public string? DESCRIPTION { get; set; }
        public int PRODUCT_QUANTITY { get; set; }
    }
}
