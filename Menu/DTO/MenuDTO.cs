using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.DTO
{
    public class MenuDTO
    {
        public int ID { get; set; }
        public string? MENU_NAME { get; set; }
        public string? MENU_LINK { get; set; }
        public int? PARENT_ID { get; set; }
        public string? MENU_STATUS { get; set; }
        public DateTime CREATED_AT { get; set; }
        public DateTime UPDATED_AT { get; set; }
    }
}
