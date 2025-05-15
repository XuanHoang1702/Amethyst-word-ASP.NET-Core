using Dapper;
using Infrastructure.IRepositoiries;
using System.Data;

namespace Infrastructure.Repositories
{
    public class UpdateData : IUpdateData
    {
        private readonly DataContext _context;
        public UpdateData(DataContext context) { _context  = context; }
        public async Task<TModel> ExceuteUpdateData<TModel>(string procedureName, object parameter) where TModel : class
        {
            using(var db = _context.CreateConnection())
            {
                return db.Query<TModel>(procedureName, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
