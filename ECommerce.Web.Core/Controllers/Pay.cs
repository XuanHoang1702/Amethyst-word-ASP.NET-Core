using Application.Share;
using Application.Share.Consts.DTO;
using Microsoft.AspNetCore.Mvc;
using Order.Intfs;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class Pay : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IFunction _function;

        public Pay(IOrderService orderService, IFunction function) 
        {
            _orderService = orderService;
            _function = function;
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveWebhook([FromBody] WebhookDTO data)
        {
            if (string.IsNullOrEmpty(data.Content))
            {
                return BadRequest(new { error = "Không tìm thấy nội dung chuyển khoản hợp lệ!" });
            }

            Console.WriteLine("📥 Nội dung chuyển khoản: " + data.Content);

            var match = Regex.Match(data.Content, @"\b(?:OR)[A-Z0-9]+\b", RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                return BadRequest(new { error = "Không tìm thấy mã đơn hàng hợp lệ trong nội dung!" });
            }

            string orderCode = match.Value.ToUpper();
            Console.WriteLine("🧾 Mã đơn hàng: " + orderCode);

            var result = await _orderService.CheckoutOnl(orderCode);
            Console.WriteLine("Kết quả: " + result.CODE);

            await _function.SendMessageAsync(orderCode, $"Đơn hàng {orderCode} đã thanh toán!");
            return Ok(new { message = "Webhook nhận thành công!" });
        }



        [HttpGet]
        public async Task Get()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                var socket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await _function.HandleSocketAsync(socket);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }

    }
}
