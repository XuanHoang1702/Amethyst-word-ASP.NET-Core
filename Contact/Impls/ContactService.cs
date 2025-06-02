using Admin.DTO;
using Application.Share;
using Application.Share.Consts;
using Contact.DTO;
using Contact.Intfs;
using Infrastructure.IRepositoiries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Impls
{
    public class ContactService : IContactService
    {
        private readonly IGetListData _getListData;
        private readonly IFunction _function;
        private readonly IUpdateData _updateData;
        private readonly ICreateData _createData;
        public ContactService(IGetListData getListData, IUpdateData updateData, IFunction function, ICreateData createData)
        {
            _getListData = getListData;
            _updateData = updateData;
            _function = function;
            _createData = createData;
        }
        public List<ContactDTO> GetList(string status)
        {
            return  _getListData.ExecuteGetListData<ContactDTO>(StoreProcedureConsts.CONTACT_List);
        }

        public async Task<ResultDTO> Create(ContactCreateDTO input)
        {
            var json = JsonConvert.SerializeObject(input);
            return await _createData.ExcuteCreateData<ResultDTO>(StoreProcedureConsts.CONTACT_Create, new { p_CONTACT_DATA_JSON = json });
        }

        public async Task<ContactDTO> Update(string token, int id, string status)
        {
            var role = _function.DeToken(token).Role;
            var result = await _updateData.ExceuteUpdateData<ContactDTO>(StoreProcedureConsts.CONTACT_Update, new { p_CONTACT_ID = id, p_ROLE = role, p_CONTACT_STATUS = status });
            return result;
        }
    }
}
