using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositoiries
{
    public interface ICreateData
    {
        public Task<TModel> ExcuteCreateData<TModel>(string procedure, object input) where TModel : class;
    }
}
