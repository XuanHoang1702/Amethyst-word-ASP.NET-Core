using Admin.DTO;
using Menu.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Intfs
{
    public interface IMenuService
    {
        public List<MenuDTO> GetList();
        public Task<object> Create(string token, MenuDTO input);
        public Task<object> Update(string token, MenuDTO input);
        public Task<ResultDTO> Delete(string token, int input);
    }
}
