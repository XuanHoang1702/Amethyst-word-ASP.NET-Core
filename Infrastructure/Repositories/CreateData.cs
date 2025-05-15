using Infrastructure.IRepositoiries;
using Dapper;
using System.Data;

namespace Infrastructure.Repositories
{
    public class CreateData : ICreateData
    {
        public readonly DataContext _context;
        public CreateData (DataContext context) {  _context = context; }
        public async Task<TModel> ExcuteCreateData<TModel>(string procedureName, object input) where TModel : class
        {
            using (var db = _context.CreateConnection())
            {
                return db.Query<TModel> (procedureName, input, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

    }
}
