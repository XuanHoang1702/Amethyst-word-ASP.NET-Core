using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositoiries
{
    public interface ILogin
    {
        public Task<TModel> ExcuteLogin<TModel>(string procedureName, object input) where TModel : class;
    }
}
