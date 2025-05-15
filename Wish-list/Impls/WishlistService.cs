using Admin.DTO;
using Application.Share;
using Application.Share.Consts;
using Infrastructure.IRepositoiries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wish_list.DTO;
using Wish_list.Intfs;

namespace Wish_list.Impls
{
    public class WishlistService : IWishlistService
    {
        private readonly IFunction _function;
        private readonly IGetListData _getListData;
        private readonly ICreateData _createData;
        private readonly IDeleteData _deleteData;
        public WishlistService(IGetListData getListData, IFunction function, ICreateData createData, IDeleteData deleteData)
        {
            _getListData = getListData;
            _function = function;
            _createData = createData;
            _deleteData = deleteData;
        }

        public List<Wish_listDTO> GetAll(string token)
        {
            string user_id = _function.DeToken(token).UserId;
            return  _getListData.ExecuteGetListDataAuth<Wish_listDTO>(StoreProcedureConsts.WISHLIST_List, new { p_USER_ID = user_id });
        }

        public async Task<ResultDTO> Create(string token, int id)
        {
            var user_id = _function.DeToken(token).UserId;

            ResultDTO result = await _createData.ExcuteCreateData<ResultDTO>(StoreProcedureConsts.WISHLIST_Create, new { p_USER_ID = user_id, p_PRODUCT_ID = id });
            return result;
        }

        public async Task<ResultDTO> Delete(string token, int id)
        {
            var user_id = _function.DeToken(token).UserId;

            var reuslt = await _deleteData.ExcuteDeleteData<ResultDTO>(StoreProcedureConsts.WISHLIST_Delete, new {p_USER_ID = user_id, p_PRODUCT_ID = id});
            return reuslt;
        }
    }
}
