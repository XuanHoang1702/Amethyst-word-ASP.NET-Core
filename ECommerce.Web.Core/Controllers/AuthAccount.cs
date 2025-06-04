using Admin.DTO;
using Application.Share;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using User.DTO;
using User.Intfs;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[Controller]/[Action]")]
    public class AuthAccount : Controller
    {
        private readonly IFunction _function;
        private readonly IUserService _userService;

        public AuthAccount(IFunction function, IUserService userService)
        {
            _function = function;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> SendOtpToEmail([FromBody] StoreOtpDTO input)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                string otpCode = new Random().Next(1000, 10000).ToString();
                ResultDTO result = await _userService.OtpStoreCreate(input.EMAIL, otpCode);
                if (result.CODE != 201)
                    return StatusCode(500, "Lỗi máy chủ");

                bool isSent = await _function.SendMailOTP(input.EMAIL, input.USER_NAME, otpCode);
                if (!isSent)
                    throw new Exception("Lỗi gửi email");

                scope.Complete();
                return Ok("OTP đã gửi qua email.");
            }

        }

    }
}
