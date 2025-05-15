using Admin.DTO;
using Application.Share;
using Application.Share.Consts;
using Infrastructure.IRepositoiries;
using Menu.DTO;
using Menu.Intfs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.Impls
{
    public class MenuService : IMenuService
    {
        private readonly IGetListData _lstData;
        private readonly ICreateData _createData;
        private readonly IDeleteData _deleteData;
        private readonly IUpdateData _updateData;
        private readonly IFunction _function;

        public MenuService(IGetListData getListData, IFunction function, ICreateData createData, IDeleteData deleteData, IUpdateData updateData) 
        {
            _lstData = getListData;
            _createData = createData;
            _deleteData = deleteData;
            _function = function;
            _updateData = updateData;
        }

        public List<MenuDTO> GetList()
        {
            var result =  _lstData.ExecuteGetListData<MenuDTO>(StoreProcedureConsts.MENU_List);
            return result;
        }

        public async Task<object> Create(string token, MenuDTO input)
        {
            var AdminId = _function.DeToken(token).UserId;
            var json = new
            {
                ADMIN_ID = AdminId,
                MENU_NAME = input.MENU_NAME,
                MENU_LINK = input.MENU_LINK,
                PARENT_ID = input.PARENT_ID,
                MENU_STATUS = input.MENU_STATUS,
            };
            var parameter = JsonConvert.SerializeObject(json);
            var result = await _createData.ExcuteCreateData<object>(StoreProcedureConsts.MENU_Create, new { p_MENU_DATA_JSON = parameter });
            return new { data = result };
        }

        public async Task<object> Update(string token, MenuDTO input)
        {
            var AdminId = _function.DeToken(token).UserId;
            var json = new
            {
                ADMIN_ID = AdminId,
                ID = input.ID,
                MENU_NAME = input.MENU_NAME,
                MENU_LINK = input.MENU_LINK,
                PARENT_ID = input.PARENT_ID,
                MENU_STATUS = input.MENU_STATUS,
            };
            var parameter = JsonConvert.SerializeObject(json);
            var result = await _updateData.ExceuteUpdateData<object>(StoreProcedureConsts.MENU_Update, new { p_MENU_DATA_JSON = parameter });
            return new { data = result };
        }

        public async Task<ResultDTO> Delete(string token, int input)
        {
            var AdminId = _function.DeToken(token).UserId;
            var json = new
            {
                ADMIN_ID = AdminId,
                ID = input
            };
            var parameter = JsonConvert.SerializeObject(json);
            var result = await _deleteData.ExcuteDeleteData<ResultDTO>(StoreProcedureConsts.MENU_Delete, new { p_MENU_DATA_JSON = parameter });
            return result;
        }
    }
}
