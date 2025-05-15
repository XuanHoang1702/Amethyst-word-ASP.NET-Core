using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.DTO
{
    public class ContactDTO
    {
        public string USER_EMAIL { get; set; }
        public string USER_NAME { get; set;}
        public string TITLE { get; set; }
        public string CONTENT {  get; set; }
        public string CONTACT_STATUS { get; set; }
        public string CREATED_AT { get; set; }
        public string UPDATED_AT { get; set; }
    }
}
