using Admin.DTO;
using Application.Share;
using Application.Share.Consts;
using Application.Share.Consts.DTO;
using Infrastructure.IRepositoiries;
using Newtonsoft.Json;
using Product.DTO;
using Product.Intfs;

namespace Product.Impls
{
    public class DiscountService : IDiscountService
    {
        private readonly IGetListData _listData;
        private readonly IFunction _function;
        private readonly ICreateData _createData;
        private readonly IUpdateData _updateData;
        private readonly IDeleteData _deleteData;
        public DiscountService(IGetListData listData, IFunction function, ICreateData createData, IUpdateData updateData, IDeleteData deleteData)

        {
            _listData = listData;
            _function = function;
            _createData = createData;
            _updateData = updateData;
            _deleteData = deleteData;
        }

        public PaginateResult<DiscountDTO> GetList(int pageNumber, int pageSize)
        {
            return _listData.ExecutePaginateData<DiscountDTO>(StoreProcedureConsts.DISCOUNT_List, new { p_PAGE_NUMBER = pageNumber, p_PAGE_SIZE = pageSize });
        }

        public async Task<object> Create(string token, DiscountInputDTO input)
        {
            var AdminId = _function.DeToken(token).UserId;
            var json = new
            {
                ADMIN_ID = AdminId,
                PRODUCT_ID = input.PRODUCT_ID,
                DISCOUNT_PERCENT = input.DISCOUNT_PERCENT,
                START_DT = input.START_DT,
                END_DT = input.END_DT
            };

            var parameter = JsonConvert.SerializeObject(json);
            var result = await _createData.ExcuteCreateData<object>(StoreProcedureConsts.DISCOUNT_Create, new { p_DISCOUNT_DATA_JSON = parameter });
            return new { data = result };
        }

        public async Task<object> Update(string token, DiscountInputDTO input)
        {
            var AdminId = _function.DeToken(token).UserId;
            var json = new
            {
                ADMIN_ID = AdminId,
                DISCOUNT_ID = input.DISCOUNT_ID,
                PRODUCT_ID = input.PRODUCT_ID,
                DISCOUNT_PERCENT = input.DISCOUNT_PERCENT,
                START_DT = input.START_DT,
                END_DT = input.END_DT
            };

            var parameter = JsonConvert.SerializeObject(json);
            var result = await _updateData.ExceuteUpdateData<object>(StoreProcedureConsts.DISCOUNT_Update, new { p_DISCOUNT_DATA_JSON = parameter });
            return new { data = result };
        }

        public async Task<ResultDTO> Delete(string token, int input)
        {
            var AdminId = _function.DeToken(token).UserId;
            var result = await _deleteData.ExcuteDeleteData<ResultDTO>(StoreProcedureConsts.DISCOUNT_Delete, new { p_ADMIN_ID = AdminId, p_DISCOUNT_ID = input });
            return result;
        }

        public async Task<List<DiscountDTO>> Home(int record)
        {
            return _listData.ExecuteGetRecordData<DiscountDTO>(StoreProcedureConsts.DISCOUNT_NoPaging, new { p_RECORD = record } );
        }
    }
}
