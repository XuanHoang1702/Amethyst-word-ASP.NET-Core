using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DTO
{
    public class DashboardDTO
    {
        public int QUANTITY {  get; set; }
        public DateTime DAY { get; set; }
        public int WEEK {  get; set; }
        public int MONTH { get; set; }
        public int YEAR { get; set; }
    }
}
