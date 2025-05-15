using Admin.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IRepositoiries
{
    public interface IDeleteData
    {
        public Task<TModel>ExcuteDeleteData<TModel>(string procedure, object input) where TModel : class;

    }
}
