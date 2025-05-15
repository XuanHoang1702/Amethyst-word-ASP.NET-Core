using Admin.DTO;
using Application.Share.Consts.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using User.DTO;

namespace User.Intfs
{
    public interface IUserService
    {
        public List<UserDTO> User_List();
        public Task<ResultDTO> User_Register(UserRegisterDTO input);
        public Task<object> User_Login(UserLoginDTO input);
        public Task<object> User_Update(UserDTO input);
        public Task<object> User_Inf(string token);
        public Task<object> RefreshToken(string input);
        public Task<ResultDTO> OtpStoreCreate(string email, string OTP);
        public Task<object> Device(string token, DeviceDTO input);
        public Task<ResultDTO> AuthAccount(AuthAccountDTO input);
        public Task<object> GoogleLogin(GoogleTokenDTO tokenDto);
    }
}
