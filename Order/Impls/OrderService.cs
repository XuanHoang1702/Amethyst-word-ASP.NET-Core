using Admin.DTO;
using Application.Share;
using Application.Share.Consts;
using Infrastructure.IRepositoiries;
using MailKit.Search;
using Newtonsoft.Json;
using Order.DTO;
using Order.Intfs;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Impls
{
    public class OrderService : IOrderService
    {
        private readonly IFunction _function;
        private readonly ICreateData _createData;
        private readonly IUpdateData _updateData;
        private readonly IGetData _getData;

        public OrderService(IFunction function, ICreateData createData, IUpdateData updateData, IGetData getData)
        {
            _function = function;
            _createData = createData;
            _updateData = updateData;
            _getData = getData;
        }

        public async Task<ResultDTO> CreateOrder(string token, OrderDTO input)
        {
            var userId = _function.DeToken(token).UserId;
            var orderId = _function.Config_ID_Order();

            var json = new
            {
                ORDER_ID = orderId,
                USER_ID = userId,
                ADDRESS_ID = input.ADDRESS_ID,
                TOTAL_QUANTITY = input.TOTAL_QUANTITY,
                TOTAL_PRICE = input.TOTAL_PRICE,
                NOTE = input.NOTE,
                ORDER_STATUS = input.ORDER_STATUS,
            };

            var parameter = JsonConvert.SerializeObject(json);
            return await _createData.ExcuteCreateData<ResultDTO>(StoreProcedureConsts.ORDER_Create, new { p_ORDER_DATA_JSON = parameter });
        }

        public async Task<ResultDTO> CreateOrderDetail(string OrderId, List<OrderDetailDTO> input)
        {
            var json = JsonConvert.SerializeObject(input);

            var result = await _createData.ExcuteCreateData<ResultDTO>(StoreProcedureConsts.ORDER_DETAIL_Create,new{ p_ORDER_ID = OrderId, p_ORDER_DETAIL_DATA_JSON = json });

            return result;
        }

        public async Task<ResultDTO> CheckoutOnl(string input)
        {
            var result = await _updateData.ExceuteUpdateData<ResultDTO>(StoreProcedureConsts.ORDER_Update, new { p_ORDER_ID = input, p_ORDER_STATUS = 2 });
            return result;
        }

        public async Task<ResultDTO> GetStatus(string token, string id)
        {
            var UserId = _function.DeToken(token).UserId;
            var json = new
            {
                USER_ID = UserId,
                ORDER_ID = id,
            };
            var parameter = JsonConvert.SerializeObject(json);
            return await _getData.ExecuteGetData<ResultDTO>(StoreProcedureConsts.ORDER_STATUS, new { p_ORDER_DATA_JSON = parameter });
        }
    }
}
