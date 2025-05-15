using Admin.DTO;
using Application.Share;
using Application.Share.Consts;
using Application.Share.Consts.DTO;
using Google.Apis.Auth;
using Infrastructure.IRepositoiries;
using Newtonsoft.Json;
using System.Security.Claims;
using User.DTO;
using User.Intfs;

namespace User.Imlps
{
    public class UserService : IUserService
    {
        private readonly IGetListData _listData;
        private readonly IFunction _function;
        private readonly ICreateData _createData;
        private readonly ILogin _login;
        private readonly IUpdateData _updateData;
        private readonly IGetData _getData;
        public UserService(IGetListData listData, IFunction function, ICreateData createData, ILogin login, IUpdateData updateData, IGetData getData)
        {
            _listData = listData;
            _function = function;
            _createData = createData;
            _updateData = updateData;
            _getData = getData;
            _login = login;
        }

        public List<UserDTO> User_List()
        {
            return _listData.ExecuteGetListData<UserDTO>(StoreProcedureConsts.USER_Lst).ToList();
        }

        public async Task<ResultDTO> User_Register(UserRegisterDTO input)
        {
            input.USER_ID = _function.Config_ID();
            input.USER_PASSWORD = _function.HashPassword(input.USER_PASSWORD);

            var json = JsonConvert.SerializeObject(input);
            var paremeter = new { p_USER_DATA_JSON = json };
            var result = await _createData.ExcuteCreateData<ResultDTO>(StoreProcedureConsts.USER_Register, paremeter);
            return result;
        }

        public async Task<object> User_Login(UserLoginDTO input)
        {
            input.USER_PASSWORD = _function.HashPassword(input.USER_PASSWORD);

            var json = JsonConvert.SerializeObject(input);
            var parameter = new { p_USER_DATA_JSON = json };

            UserDTO result = await _login.ExcuteLogin<UserDTO>(StoreProcedureConsts.USER_Login, parameter);
            if(result != null)
            {
                var token = _function.GenerateToken(result.USER_ID, result.USER_LAST_NAME, result.USER_EMAIL, result.USER_PHONE);
                var refreshToken = _function.GenerateRefreshToken();

                var resultRefresh = await _createData.ExcuteCreateData<ResultDTO>(StoreProcedureConsts.REFRESH_TOKEN_Create, new { p_USER_ID = result.USER_ID, p_TOKEN = refreshToken, p_EXPITY_DATE = DateTime.UtcNow.AddDays(7) });
                if (resultRefresh.CODE != 201)
                {
                    return new { CODE = 500, message = "Có lỗi xả ra" };
                }
                return new { CODE = 200, message = "Đăng nhập thành công", TOKEN = token, REFRESH_TOKEN = refreshToken };
            }
            return new { CODE = 401, message = "Tài khoản không tồn tại hoặc mật khẩu sai" };
        }

        public async Task<object> User_Update(UserDTO input)
        {
            var json = JsonConvert.SerializeObject(input);
            var parameter = new { p_USER_DATA_JSON = json };

            UserDTO result = await _updateData.ExceuteUpdateData<UserDTO>(StoreProcedureConsts.USER_Up, parameter);
            if(result != null)
            {
                return new { Code = 200, message = "Cập nhật thông tin thành công",  data = result };
            }
            return new { Code = 204, message = "Cập nhật thông tin thất bại" };
        }

        public async Task<object> User_Inf(string token)
        {
            var data = _function.DeToken(token);

            var reulst = await _getData.ExecuteGetData<object>(StoreProcedureConsts.USER_Inf, new { p_USER_ID = data.UserId });

            Object response = new { user_Inf = reulst, role = data.Role};
            return response;
        }

        public async Task<object> RefreshToken(string input)
        {
            ResultDTO refreshToken = await _getData.ExecuteGetData<ResultDTO>(StoreProcedureConsts.REFRESH_TOKEN_ById, new { p_TOKEN = input });

            if (refreshToken.CODE != 200)
            {
                return new { refreshToken };
            }

            var user = await _getData.ExecuteGetData<UserDTO>(StoreProcedureConsts.USER_Inf, new { p_USER_ID  = refreshToken.RESULT });
            var newRefreshToken = _function.GenerateRefreshToken();

            ResultDTO result = await _updateData.ExceuteUpdateData<ResultDTO>(StoreProcedureConsts.REFRESH_TOKEN_Update, new { p_USER_ID = user.USER_ID, p_TOKEN = newRefreshToken });
            if (result.CODE != 200)
            {
                return new { result };
            }
            var newAccessToken = _function.GenerateToken(user.USER_ID, user.USER_LAST_NAME, user.USER_EMAIL, user.USER_PHONE);
            return new { TOKEN = newAccessToken, REFRESH_TOKEN = newRefreshToken };
        }

        public async Task<ResultDTO> OtpStoreCreate(string email, string OTP)
        {
            ResultDTO result = await _createData.ExcuteCreateData<ResultDTO>(StoreProcedureConsts.OTP_STORE_Create, new { p_EMAIL = email, p_OTP = OTP });
            return result;
        }

        public async Task<object> Device(string token, DeviceDTO input)
        {
            var userId = _function.DeToken(token).UserId;
            var json = new
            {
                USER_ID = userId,
                VISITOR_ID = input.VISITOR_ID,
                DEVICE_INFO = JsonConvert.SerializeObject(input.DEVICE_INFO),
                AUTH_CODE = input.AUTH_CODE,
            };
            var parameter = JsonConvert.SerializeObject(json);

            ResultDTO result = await _createData.ExcuteCreateData<ResultDTO>(StoreProcedureConsts.DEVICES_Check, new { @p_DEVICE_DATA_JSON = parameter });
            return result;
        }

        public async Task<ResultDTO> AuthAccount(AuthAccountDTO input)
        {
            ResultDTO result = await _getData.ExecuteGetData<ResultDTO>(StoreProcedureConsts.OTP_STORE_Check, new { p_EMAIL  = input.Email, p_OTP =  input.OTP });
            return result;
        }

        public async Task<object> GoogleLogin(GoogleTokenDTO tokenDto)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(tokenDto.Credential);

            string email = payload.Email;
            string name = payload.Name;


            if (string.IsNullOrEmpty(email))
            {
                return new { message = "Không tìm thấy tài khoản", code = 401 };
            }

            var userId = _function.Config_ID();
            ResultDTO result = await _createData.ExcuteCreateData<ResultDTO>(StoreProcedureConsts.USER_UPSERT_Google, new
            {
                p_USER_ID = userId,
                p_LAST_NAME = name,
                p_EMAIL = email,
            });

            if (result.CODE != 200)
            {
                return new { CODE = result.CODE, MESSAGE = result.MESSAGE};
            }

            var token = _function.GenerateToken(userId, name, email);
            return new { CODE = result.CODE, MESSAGE = result.MESSAGE, TOKEN = token };
        }
    }
}
