using Admin.DTO;
using Category.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Category.Intfs
{
    public interface ICategoryService
    {
        public List<CategoryDTO> GetList();
        public Task<object> Create(string token, CategoryCreateDTO input);
        public Task<object> Update(string token, CategoryUpdateDTO input);
        public Task<ResultDTO> Delete(string token, int id);
    }
}
