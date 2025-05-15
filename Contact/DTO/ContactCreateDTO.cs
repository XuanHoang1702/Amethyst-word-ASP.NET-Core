using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.DTO
{
    public class ContactCreateDTO
    {
        public string USER_EMAIL { get; set; }
        public string TITLE { get; set; }
        public string CONTENT {  get; set; }
    }
}
