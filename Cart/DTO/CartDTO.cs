using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.DTO
{
    public class CartDTO
    {
        public int? PRODUCT_ID {  get; set; }
        public string PRODUCT_NAME { get; set; }
        public decimal PRODUCT_PRICE { get; set; }
        public string IMAGE_NAME { get; set; }
        public int? QUANTITY {  get; set; }
    }
}
