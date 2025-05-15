using Admin.DTO;
using Microsoft.AspNetCore.Mvc;
using Wish_list.DTO;
using Wish_list.Intfs;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class Wish_listController : Controller
    {
        private readonly IWishlistService _wishlistService;
        public Wish_listController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpGet]
        public List<Wish_listDTO> GetAll([FromHeader(Name = "Authorization")] string token)
        {
            if(token.StartsWith("Bearer "))
                token = token.Substring(7);
            return _wishlistService.GetAll(token);
        }

        [HttpPost]
        public async Task<ResultDTO> Create([FromHeader(Name = "Authorization")] string token,[FromBody] int id)
        {
            if (token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _wishlistService.Create(token, id);
        }

        [HttpDelete]
        public async Task<ResultDTO> Delete([FromHeader(Name = "Authorization")] string token,[FromBody] int id)
        {
            if (token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _wishlistService.Delete(token, id);

        }
    }
}
