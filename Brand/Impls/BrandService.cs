using Admin.DTO;
using Application.Share;
using Application.Share.Consts;
using Brand.DTO;
using Brand.Intfs;
using Infrastructure.IRepositoiries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brand.Impls
{
    public class BrandService : IBrandService
    {
        private readonly IGetListData _lstData;
        private readonly IFunction _function;
        private readonly ICreateData _createData;
        private readonly IDeleteData _deleteData;
        public BrandService(IGetListData lstData, IFunction function, ICreateData createData, IDeleteData deleteData)
        {
            _lstData = lstData;
            _function = function;
            _createData = createData;
            _deleteData = deleteData;
        }

        public  List<BrandDTO> GetList()
        {
            var result =  _lstData.ExecuteGetListData<BrandDTO>(StoreProcedureConsts.BRAND_List);
            return result;
        }

        public async Task<ResultDTO> Create(string token, BrandCreateDTO input)
        {
            var id = _function.DeToken(token).UserId;
            object customInput = new
            {
                ADMIN_ID = id,
                BRAND_NAME = input.BRAND_NAME,
                BRAND_IMAGE = input.BRAND_IMAGE,
                BRAND_STATUS = input.BRAND_STATUS,
                DESCRIPTION = input.DESCRIPTION,
            };
            var json = JsonConvert.SerializeObject(customInput);
            var parameter = new { p_BRAND_DATA_JSON = json };

            return await _createData.ExcuteCreateData<ResultDTO>(StoreProcedureConsts.BRAND_Create, parameter);
        }

        public async Task<ResultDTO> Update(string token, BrandUpdateDTO input)
        {
            var id = _function.DeToken(token).UserId;
            object customInput = new
            {
                ADMIN_ID = id,
                BRAND_ID = input.BRAND_ID,
                BRAND_NAME = input.BRAND_NAME,
                BRAND_IMAGE = input.BRAND_IMAGE,
                BRAND_STATUS = input.BRAND_STATUS,
                DESCRIPTION = input.DESCRIPTION,
            };
            var json = JsonConvert.SerializeObject(customInput);
            var parameter = new { p_BRAND_DATA_JSON = json };

            return await _createData.ExcuteCreateData<ResultDTO>(StoreProcedureConsts.BRAND_Update, parameter);
        }

        public async Task<ResultDTO> Delete(string token, int id)
        {
            var AdminId = _function.DeToken(token).UserId;
            object input = new
            {
                ADMIN_ID = AdminId,
                BRAND_ID = id
            };
            var json = JsonConvert.SerializeObject(input);
            var parameter = new { p_BRAND_DATA_JSON = json};

            var result = await _deleteData.ExcuteDeleteData<ResultDTO>(StoreProcedureConsts.Brand_Delete, parameter);

            return result;

        }

    }
}
