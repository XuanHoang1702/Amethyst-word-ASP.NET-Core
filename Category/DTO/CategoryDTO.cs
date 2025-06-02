using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Category.DTO
{
    public class CategoryDTO
    {
        public int CATEGORY_ID { get; set; }
        public string? CATEGORY_NAME { get; set; }
        public string? CATEGORY_IMAGE { get; set; }
        public string? CATEGORY_STATUS { get; set; }
        public string? ICON_NAME { get; set; }
        public string? ICON_COLOR { get; set; }
        public DateTime CREATED_AT { get; set; }
        public DateTime UPDATE_AT { get; set; }
        public int? PRODUCT_QUANTITY { get; set; }
        public string? DESCRIPTION { get; set; }
    }
}
