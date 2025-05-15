using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Infrastructure.IRepositoiries;

namespace Infrastructure.Repositories
{
    public class Login : ILogin
    {
        private readonly DataContext _dataContext;
        public Login(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<TModel> ExcuteLogin<TModel>(string procedureName, object input) where TModel : class
        {
            using(var db = _dataContext.CreateConnection())
            {
                return db.Query<TModel>(procedureName, input, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

    }
}
