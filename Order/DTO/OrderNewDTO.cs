using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.DTO
{
    public class OrderNewDTO
    {
        public string? ID { get; set; }
        public string? USER_LAST_NAME { get; set; }
        public decimal? TOTAL_PRICE { get; set; }
        public int? ORDER_STATUS { get; set; }
        public DateTime? CREATED_AT { get; set; }
    }
}
