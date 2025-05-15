using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositoiries
{
    public interface IGetData
    {
        public Task<TModel> ExecuteGetData<TModel>(string procedureName, object parameter) where TModel : class;
    }
}
