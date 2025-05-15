using System.Data;
using Application.Share.Consts.DTO;
using Dapper;
using Infrastructure.IRepositoiries;

namespace Infrastructure.Repositories
{
    public class GetListData : IGetListData
    {
        private readonly DataContext _context;

        public GetListData(DataContext context)
        {
            _context = context;
        }

        public List<TModel> ExecuteGetListDataAuth<TModel>(string procedureName, object parameter) where TModel : class
        {
            using (var db = _context.CreateConnection())
            {
                return db.Query<TModel>(procedureName, parameter, commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public List<TModel> ExecuteGetListData<TModel>(string procedureName) where TModel : class
        {
            using (var db = _context.CreateConnection())
            {
                return db.Query<TModel>(procedureName, commandType: CommandType.StoredProcedure).AsList();
            }
        }

        public PaginateResult<TModel> ExecutePaginateData<TModel>(string procedureName, object parameter) where TModel : class
        {
            using (var db = _context.CreateConnection())
            {
                var parameters = new DynamicParameters(parameter);
                parameters.Add("p_TOTAL_RECORD", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var reuslt =  db.Query<TModel>(procedureName, parameters, commandType: CommandType.StoredProcedure).AsList();
                return new PaginateResult<TModel>
                {
                    DATA = reuslt,
                    TOTAL_RECORD = parameters.Get<int>("p_TOTAL_RECORD")
                };
            }
        }

        public List<TModel> ExeciteGetListDataById<TModel>(string procedureName, object parameter) where TModel : class
        {
            using (var db = _context.CreateConnection())
            {
                return db.Query<TModel>(procedureName, parameter, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<TModel> ExecuteGetRecordData<TModel>(string procedureName, object parameter) where TModel : class
        {
            using (var db = _context.CreateConnection())
            {
                return db.Query<TModel>(procedureName, parameter, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
