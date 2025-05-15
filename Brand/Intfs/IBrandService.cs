using Admin.DTO;
using Brand.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brand.Intfs
{
    public interface IBrandService
    {
        public List<BrandDTO> GetList();
        public Task<object> Create(string token, BrandCreateDTO input);
        public Task<object> Update(string token, BrandUpdateDTO input);
        public Task<ResultDTO> Delete(string token, int id);
    }
}
