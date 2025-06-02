using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.DTO
{
    public class CartCreateDTO
    {
        public int? PRODUCT_ID { get; set; }
        public int COLOR_ID { get; set; }
        public int SIZE_ID { get; set; }
        public int? QUANTITY { get; set; }
    }
}
