using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.DTO
{
    public class DeviceDTO
    {
        public string? VISITOR_ID { get; set; }
        public MobileDTO? DEVICE_INFO { get; set; }
        public string? AUTH_CODE { get; set; }
    }

    public class MobileDTO
    {
        public string? DIVECE_NAME { get; set; }
        public string? MODEL {  get; set; }
        public string? OS {  get; set; }

    }
}
