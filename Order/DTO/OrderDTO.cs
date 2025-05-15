using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DTO
{
    public class OrderDTO
    {
        public string? ID { get; set; }
        public string? USER_ID { get; set; }
        public int? ADDRESS_ID { get; set; }
        public int? TOTAL_QUANTITY { get; set; }
        public decimal? TOTAL_PRICE { get; set; }
        public string? NOTE { get; set; }
        public int? ORDER_STATUS { get; set; }
    }
}
