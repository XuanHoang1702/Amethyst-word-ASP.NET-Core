using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DTO
{
    public class OrderDetailDTO
    {
        public int PRODUCT_ID { get; set; }
        public int QUANTITY {  get; set; }
        public decimal PRODUCT_PRICE { get; set; }
        public decimal SUBTOTAL { get; set; }
    }
}
