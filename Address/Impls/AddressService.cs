using Address.DTO;
using Address.Intfs;
using Application.Share;
using Application.Share.Consts;
using Infrastructure.IRepositoiries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Address.Impls
{
    public class AddressService : IAddressService
    {
        private readonly IFunction _function;
        private readonly IGetListData _getListData;
        private readonly ICreateData _createData;
        private readonly IUpdateData _updateData;
        public AddressService(IFunction function, IGetListData getListData, ICreateData createData, IUpdateData updateData)
        {
            _function = function;
            _getListData = getListData;
            _createData = createData;
            _updateData = updateData;
        }
        public List<AddressDTO> GetAddresses(string token)
        {
            var userId = _function.DeToken(token).UserId;
            var result = _getListData.ExecuteGetListDataAuth<AddressDTO>(StoreProcedureConsts.ADDRESS_Get, new { p_USER_ID = userId }).ToList();
            return result;
        }
        public async Task<object> CreateAddress(string token, AddressDTO input)
        {
            var userId = _function.DeToken(token).UserId;

            var inputData = new
            {
                USER_ID = userId,
                input.HOUSE_NUMBER,
                input.STREET,
                input.CITY,
                input.POSTAL_CODE,
                input.COUNTRY,
                input.TYPE_ADDRESS
            };

            var json = JsonConvert.SerializeObject(inputData);
            var parameter = new { p_ADDRESS_DATA_JSON = json };

            var result = _createData.ExcuteCreateData<object>(StoreProcedureConsts.ADDRESS_Create, parameter);
          
            return new { data = result};
            
        }

        public async Task<object> UpdateAddress(string token, AddressDTO input)
        {
            var userId = _function.DeToken(token).UserId;

            var inputData = new
            {
                USER_ID = userId,
                input.HOUSE_NUMBER,
                input.STREET,
                input.CITY,
                input.POSTAL_CODE,
                input.COUNTRY,
                input.TYPE_ADDRESS
            };

            var json = JsonConvert.SerializeObject(inputData);
            var parameter = new { p_ADDRESS_DATA_JSON = json };
            var result = _updateData.ExceuteUpdateData<object>(StoreProcedureConsts.ADDRESS_Up, parameter);

            return new { data = result };
          
        }
    }
}

