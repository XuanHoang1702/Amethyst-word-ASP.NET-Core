using Admin.DTO;
using Cart.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Intfs
{
    public interface ICartService
    {
        public List<CartDTO> GetAll(string token);
        public Task<ResultDTO> Create(string token, CartDTO input);
        public Task<ResultDTO> Delete(string token, int input);
    }
}
