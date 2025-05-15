using Admin.DTO;
using Microsoft.AspNetCore.Mvc;
using Order.DTO;
using Order.Intfs;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ResultDTO> CreateOrder([FromHeader(Name = "Authorization")] string token, [FromBody] OrderDTO input)
        {
            if(token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _orderService.CreateOrder(token, input);
        }

        [HttpPost]
        public async Task<ResultDTO> CreateOrderDetail(string OrderId, [FromBody] List<OrderDetailDTO> input) => await _orderService.CreateOrderDetail(OrderId, input);

        [HttpGet]
        public async Task<ResultDTO> GetStatus([FromHeader(Name = "Authorization")] string token, string id)
        {
            if(token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _orderService.GetStatus(token, id);
        }
    }
}
