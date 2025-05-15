using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address.DTO
{
    public class AddressDTO
    {
        public int ID {  get; set; }
        public string HOUSE_NUMBER { get; set; }
        public string STREET { get; set; }
        public string CITY { get; set; }
        public string POSTAL_CODE { get; set; }
        public string COUNTRY { get; set; }
        public string TYPE_ADDRESS { get; set; }
    }
}
