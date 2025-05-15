using Admin.DTO;
using Admin.Intfs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]

    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        public async Task<object> Login([FromBody] AdminLoginDTO input) => await _adminService.Login(input);

        [HttpPost]
        public async Task<object> Create([FromHeader( Name = "Authorization")] string token, [FromBody] CreateAdminDTO input)
        {
            if(token.StartsWith("Bearer "))
            {
                token = token.Substring(7);
            }
            return await _adminService.Create_Account(token, input);
        }

        [HttpDelete]
        public async Task<ResultDTO> Delete([FromHeader( Name = "Authorization")] string token, [FromBody] string input)
        {
            if (token.StartsWith("Bearer"))
            {
                token = token.Substring(7);
            }
            return await _adminService.Delete_Account(token, input);
        }
    }
}
