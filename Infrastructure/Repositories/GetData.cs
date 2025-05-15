using Dapper;
using Infrastructure.IRepositoiries;
using System.Data;

namespace Infrastructure.Repositories
{
    public class GetData : IGetData
    {
        private readonly DataContext _context;
        public GetData(DataContext context) { _context = context; }
        public async Task<TModel> ExecuteGetData<TModel>(string procedureName, object parameter) where TModel : class
        {
            using(var db = _context.CreateConnection())
            {
                return db.Query<TModel>(procedureName, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

    }
}
