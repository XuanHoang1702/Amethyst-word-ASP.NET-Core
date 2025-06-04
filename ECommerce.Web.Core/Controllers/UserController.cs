using Admin.DTO;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using User.DTO;
using User.Intfs;
using System.Web;
using Microsoft.Data.SqlClient;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _environment;
        public UserController(IUserService userService, IWebHostEnvironment environment)
        {
            _userService = userService;
            _environment = environment;
        }

        [HttpGet]
        public ActionResult<List<UserDTO>> GetList() => _userService.User_List();

        [HttpPost]
        public async Task<ResultDTO> Register([FromBody] UserRegisterDTO input) => await _userService.User_Register(input);

        [HttpPost]
        public async Task<object> Login([FromBody] UserLoginDTO input) => await _userService.User_Login(input);

        [HttpPut]
        public async Task<object> Update([FromBody] UserDTO input) => await _userService.User_Update(input);

        [HttpGet]
        public async Task<object> Information([FromHeader(Name = "Authorization")] string token)
        {
            if (token.StartsWith("Bearer "))
            {
                token = token.Substring(7);
            }
            return await _userService.User_Inf(token);
        }

        [HttpPost]
        public async Task<object> RefreshToken([FromBody] string input) => await _userService.RefreshToken(input);

        [HttpPost]
        public async Task<object> CheckDevice([FromHeader(Name = "Authorization")] string token, [FromBody] DeviceDTO input)
        {
            if(token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _userService.Device(token, input);
        }

        [HttpPost]
        public async Task<ResultDTO> AuthAccount([FromBody] AuthAccountDTO input) => await _userService.AuthAccount(input);

        [HttpPost]
        public async Task<object> GoogleResponse([FromBody] GoogleTokenDTO tokenDto) =>  await _userService.GoogleLogin(tokenDto);
    }
}
