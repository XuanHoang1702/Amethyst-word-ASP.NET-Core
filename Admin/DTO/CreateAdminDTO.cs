using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.DTO
{
    public class CreateAdminDTO
    {
        public string ADMIN_NAME { get; set; }
        public string ADMIN_EMAIL { get; set; }
        public string ADMIN_PHONE { get; set; }
        public string ADMIN_PASSWORD { get; set; }
        public string ROLE { get; set; }
    }
}
