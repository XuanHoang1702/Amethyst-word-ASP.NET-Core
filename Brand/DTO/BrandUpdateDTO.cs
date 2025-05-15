using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brand.DTO
{
    public class BrandUpdateDTO
    {
        public int? BRAND_ID { get; set; }
        public string? BRAND_NAME { get; set; }
        public string? BRAND_IMAGE { get; set; }
        public string? BRAND_STATUS { get; set; }

    }
}
