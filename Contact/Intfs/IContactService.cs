using Contact.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Intfs
{
    public interface IContactService
    {
        public List<ContactDTO> GetList(string status);
        public Task<object> Create(ContactCreateDTO input);
        public Task<ContactDTO> Update(string token, int id, string status);
    }
}
