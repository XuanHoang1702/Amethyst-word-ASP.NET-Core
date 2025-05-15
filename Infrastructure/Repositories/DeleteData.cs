using Admin.DTO;
using Dapper;
using Infrastructure.IRepositoiries;
using System.Data;

namespace Infrastructure.Repositories
{
    public class DeleteData : IDeleteData
    {
        private readonly DataContext _context;

        public DeleteData(DataContext context)
        {
            _context = context;
        }
        public async Task<TModel> ExcuteDeleteData<TModel>(string procedure, object input) where TModel : class 
        {
            using(var db = _context.CreateConnection())
            {
                return db.Query<TModel>(procedure, input, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
