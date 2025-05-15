using Admin.DTO;
using Application.Share;
using Application.Share.Consts;
using Cart.DTO;
using Cart.Intfs;
using Infrastructure.IRepositoiries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cart.Impls
{
    public class CartService : ICartService
    {
        private readonly IGetListData _lstData;
        private readonly IFunction _function;
        private readonly ICreateData _createData;
        private readonly IDeleteData _deleteData;
        public CartService(IGetListData lstData, ICreateData createData, IFunction function, IDeleteData deleteData)
        {
            _lstData = lstData;
            _createData = createData;
            _function = function;
            _deleteData = deleteData;
        }
        public List<CartDTO> GetAll(string token)
        {
            var userId = _function.DeToken(token).UserId;
            var result = _lstData.ExeciteGetListDataById<CartDTO>(StoreProcedureConsts.CART_List, new { p_USER_ID = userId});
            return result;
        }

        public async Task<ResultDTO> Create(string token, CartDTO input)
        {
            var userId = _function.DeToken(token).UserId;
            var json = new 
            {
                USER_ID = userId,
                PRODUCT_ID = input.PRODUCT_ID,
                QUANTITY = input.QUANTITY,
            };
            var parameter = JsonConvert.SerializeObject(json);
            var result = await _createData.ExcuteCreateData<ResultDTO>(StoreProcedureConsts.CART_Create, new { p_CART_DATA_JSON  =  parameter });
            return result;
        }

        public async Task<ResultDTO> Delete(string token, int input)
        {
            var userId = _function.DeToken(token).UserId;
            var json = new
            {
                USER_ID = userId,
                PRODUCT_ID = input,
            };
            var parameter = JsonConvert.SerializeObject(json);
            var result = await _deleteData.ExcuteDeleteData<ResultDTO>(StoreProcedureConsts.CART_Delete, new { p_CART_DATA_JSON = parameter });
            return result;
        }
    }
}
