using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.DTO
{
    public class DiscountInputDTO
    {
        public int? DISCOUNT_ID { get; set; }
        public int PRODUCT_ID { get; set; }
        public decimal DISCOUNT_PERCENT { get; set; }
        public DateTime START_DT { get; set; }
        public DateTime END_DT { get; set; }
    }
}
