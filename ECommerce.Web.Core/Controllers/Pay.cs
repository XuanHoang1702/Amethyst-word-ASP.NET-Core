using Microsoft.AspNetCore.Mvc;
using Order.Intfs;
using System.Text.Json;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class Pay : Controller
    {
        private readonly IOrderService _orderService;

        public Pay(IOrderService orderService) 
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveWebhook([FromBody] JsonElement data)
        {
            if (!data.TryGetProperty("content", out JsonElement contentElement) || contentElement.ValueKind != JsonValueKind.String)
            {
                return BadRequest(new { error = "Không tìm thấy nội dung chuyển khoản hợp lệ!" });
            }

            string content = contentElement.GetString()!;
            Console.WriteLine("📥 Nội dung chuyển khoản: " + content);
            string[] parts = content.Split('-');

            if (parts.Length < 2)
            {
                return BadRequest(new { error = "Định dạng nội dung không hợp lệ!" });
            }

            string orderCode = parts[1];
            Console.WriteLine("🧾 Mã đơn hàng: " + orderCode);

            var result = await _orderService.CheckoutOnl(orderCode);
            Console.WriteLine("Kết quả: " + result);
            return Ok(new { message = "Webhook nhận thành công!" });
        }
    }
}
