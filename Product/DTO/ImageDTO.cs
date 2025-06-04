using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.DTO
{
    public class ImageDTO
    {
        public int? ID {  get; set; }
        public string? IMAGE_NAME { get; set; }
        public int? PRODUCT_ID { get; set; }
        public string? IMAGE_STATUS {  get; set; }
    }
}
