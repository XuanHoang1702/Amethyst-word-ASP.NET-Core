using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.DTO
{
    public class RefreshTokenDTO
    {
        public int ID { get; set; }
        public string USER_ID { get; set; }
        public string TOKEN {  get; set; }
        public DateTime EXPITY_DATE { get; set; }
    }
}
