using Admin.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wish_list.DTO;

namespace Wish_list.Intfs
{
    public interface IWishlistService
    {
        public List<Wish_listDTO> GetAll(string token);
        public Task<ResultDTO> Create(string token, int id);
        public Task<ResultDTO> Delete(string token, int id);
    }
}
