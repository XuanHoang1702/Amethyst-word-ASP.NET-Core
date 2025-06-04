using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.DTO
{
    public class RateCommentDTO
    {
        public string? USER_ID { get; set; }
        public string? USER_LAST_NAME { get; set; }
        public int? PRODUCT_ID { get; set; }
        public int? RATE {  get; set; }
        public string? COMMENT { get; set; }
    }
}
