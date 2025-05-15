using Admin.DTO;
using Admin.Intfs;
using Application.Share;
using Application.Share.Consts;
using Infrastructure.IRepositoiries;
using Newtonsoft.Json;

namespace Admin.Imlps
{
    public class AdminService : IAdminService
    {
        private readonly IFunction _function;
        private readonly ILogin _login;
        private readonly ICreateData _createData;
        private readonly IDeleteData _deleteData;
        public AdminService (IFunction function, ILogin login, ICreateData createData, IDeleteData deleteData)
        {
            _function = function;
            _login = login;
            _createData = createData;
            _deleteData = deleteData;
        }
         
        public async Task<object> Login(AdminLoginDTO input)
        {
            input.ADMIN_PASSWORD = _function.HashPassword(input.ADMIN_PASSWORD);

            var json = JsonConvert.SerializeObject(input);
            var parameter = new { p_ADMIN_DATA_JSON = json };
            AdminDTO result = await _login.ExcuteLogin<AdminDTO>(StoreProcedureConsts.ADMIN_Login, parameter);

            if(result != null)
            {
                var token = _function.GenerateToken(result.ADMIN_ID, result.ADMIN_NAME, result.ADMIN_EMAIL, result.ADMIN_PHONE, result.ROLE);
                return new { CODE = 200, TOKEN = token };
            }
            return new { CODE = 401, message = "Tài khoản không tồn tại hoặc mật khẩu không đúng" };
        }

        public async Task<object> Create_Account(string token, CreateAdminDTO input)
        {
            var id = _function.DeToken(token).UserId;
            object customInput = new
            {
                ID = _function.Config_ID_Admin(),
                ADMIN_NAME = input.ADMIN_NAME,
                ADMIN_EMAIL = input.ADMIN_EMAIL,
                ADMIN_PHONE = input.ADMIN_PHONE,
                ADMIN_PASSWORD = _function.HashPassword(input.ADMIN_PASSWORD),
                ROLE = input.ROLE,
                ADMIN_ID = id
            };

            var json = JsonConvert.SerializeObject(customInput);
            var parameter = new { p_ADMIN_DATA_JSON = json };
            AdminDTO result = await _createData.ExcuteCreateData<AdminDTO>(StoreProcedureConsts.ADMIN_Create_Acount, parameter);

            return new { data = result };

        }

        public async Task<ResultDTO> Delete_Account(string token, string id)
        {
             var decode = _function.DeToken(token);
             var json = JsonConvert.SerializeObject(new { ADMIN_ID = id, ROLE_ACTION = decode.Role });
             var parameter = new { p_ADMIN_DATA_JSON = json };
             var result = await _deleteData.ExcuteDeleteData< ResultDTO>(StoreProcedureConsts.ADMIN_Des, parameter);

             return result ;
        }
    }
}
