using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.DTO
{
    public class StockDTO
    {
        public int? ID {  get; set; }
        public int? PRODUCT_ID {  get; set; }
        public decimal? PRICE_ROOT {  get; set; }
        public int? QUANTITY {  get; set; }
        public DateTime CREATED_AT { get; set; }
        public DateTime UPDATED_AT { get; set; }
    }
}
