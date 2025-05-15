using Admin.DTO;
using Cart.DTO;
using Cart.Intfs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public List<CartDTO> GetList([FromHeader(Name = "Authorization")] string token)
        {
            if (token.StartsWith("Bearer "))
                token = token.Substring(7);
            return _cartService.GetAll(token);
        }

        [HttpPost]
        public async Task<ResultDTO> Create([FromHeader(Name = "Authorization")] string token,[FromBody] CartDTO input)
        {
            if (token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _cartService.Create(token, input);
        }

        [HttpDelete]
        public async Task<ResultDTO> Delete([FromHeader(Name = "Authorization")] string token, [FromBody] int input)
        {
            if(token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _cartService.Delete(token, input);
        }
    }
}
