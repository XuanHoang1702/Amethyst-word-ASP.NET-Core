using Application.Share.Consts.DTO;

namespace Infrastructure.IRepositoiries
{
    public interface IGetListData
    {
        public List<TModel> ExecuteGetListDataAuth<TModel>(string procedureName, object parameter) where TModel : class;
        public List<TModel> ExecuteGetListData<TModel>(string procedureName) where TModel : class;
        public List<TModel> ExecuteGetRecordData<TModel>(string procedureName, object parameter) where TModel : class;
        public PaginateResult<TModel> ExecutePaginateData<TModel>(string procedureName, object parameter) where TModel : class;
        public List<TModel> ExeciteGetListDataById<TModel>(string procedureName, object parameter) where TModel: class;
    }
}
