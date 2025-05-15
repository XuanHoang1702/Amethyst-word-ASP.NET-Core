using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.DTO
{
    public class AuthAccountDTO
    {
        public string Email { get; set; }
        public string OTP {  get; set; }
    }

    public class GoogleTokenDTO
    {
        public string Credential { get; set; }
    }

}
