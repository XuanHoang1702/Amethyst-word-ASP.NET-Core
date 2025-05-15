using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace User.DTO
{
    public class UserRegisterDTO
    {
        public string USER_ID {  get; set; }
        public string USER_FIRST_NAME { get; set; }
        public string USER_LAST_NAME { get; set; }
        public string? USER_GENDER { get; set; }
        public string USER_EMAIL { get; set; }
        public string? USER_PHONE { get; set; }
        public string USER_PASSWORD { get; set; }
        public string? PROVIDER {  get; set; }
    }
}
