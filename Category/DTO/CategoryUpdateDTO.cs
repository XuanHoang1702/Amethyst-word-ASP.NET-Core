using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Category.DTO
{
    public class CategoryUpdateDTO
    {
        public int CATEGORY_ID {  get; set; }
        public string? CATEGORY_NAME { get; set; }
        public string? CATEGORY_IMAGE { get; set; }
        public string? CATEGORY_STATUS { get; set; }

    }
}
