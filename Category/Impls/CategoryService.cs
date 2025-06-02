using Admin.DTO;
using Application.Share;
using Application.Share.Consts;
using Category.DTO;
using Category.Intfs;
using Infrastructure.IRepositoiries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Category.Impls
{
    public class CategoryService : ICategoryService
    {
        private readonly IGetListData _lstData;
        private readonly ICreateData _createData;
        private readonly IFunction _function;
        private readonly IUpdateData _updateData;
        private readonly IDeleteData _deleteData;

        public CategoryService(IGetListData lstData, ICreateData createData, IFunction function, IUpdateData updateData, IDeleteData deleteData)
        {
            _lstData = lstData;
            _createData = createData;
            _function = function;
            _updateData = updateData;
            _deleteData = deleteData;
        }

        public List<CategoryDTO> GetList()
        {
            var result = _lstData.ExecuteGetListData<CategoryDTO>(StoreProcedureConsts.CATEGORY_List);
            return result;
        }

        public async Task<ResultDTO> Create(string token, CategoryCreateDTO input)
        {
            var id = _function.DeToken(token).UserId;
            object customInput = new
            {
                ADMIN_ID = id,
                CATEGORY_NAME = input.CATEGORY_NAME,
                CATEGORY_IMAGE = input.CATEGORY_IMAGE,
                CATEGORY_STATUS = input.CATEGORY_STATUS,
                DESCRIPTION = input.DESCRIPTION
            };

            var json = JsonConvert.SerializeObject(customInput);
            var parameter = new { p_CATEGORY_DATA_JSON = json };
            return await _createData.ExcuteCreateData<ResultDTO>(StoreProcedureConsts.CATEGORY_Create, parameter);
            
        }

        public async Task<ResultDTO> Update(string token, CategoryUpdateDTO input)
        {
            var id = _function.DeToken(token).UserId;
            object customInput = new
            {
                ADMIN_ID = id,
                CATEGORY_ID = input.CATEGORY_ID,
                CATEGORY_NAME = input.CATEGORY_NAME,
                CATEGORY_IMAGE = input.CATEGORY_IMAGE,
                CATEGORY_STATUS = input.CATEGORY_STATUS,
                DESCRIPTION = input.DESCRIPTION
            };

            var json = JsonConvert.SerializeObject(customInput);
            var parameter = new { p_CATEGORY_DATA_JSON = json };
            return await _updateData.ExceuteUpdateData<ResultDTO>(StoreProcedureConsts.CATEGORY_Update, parameter);

        }

        public async Task<ResultDTO> Delete(string token, int id)
        {
            var AdminId = _function.DeToken(token).UserId;
            object customInput = new
            {
                ADMIN_ID = AdminId,
                CATEGORY_ID = id,
            };

            var json = JsonConvert.SerializeObject(customInput);
            var parameter = new { p_CATEGORY_DATA_JSON = json };
            var result = await _deleteData.ExcuteDeleteData<ResultDTO>(StoreProcedureConsts.CATEGORY_Delete, parameter);

            return result ;
            
        }

    }
}
