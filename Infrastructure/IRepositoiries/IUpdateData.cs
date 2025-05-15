using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositoiries
{
    public interface IUpdateData
    {
        public Task<TModel> ExceuteUpdateData<TModel>(string procedureName, object parameter) where TModel : class;
    }
}
