using Admin.DTO;
using Menu.DTO;
using Menu.Intfs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public List<MenuDTO> GetList() => _menuService.GetList();

        [HttpPost]
        public async Task<object> Create([FromHeader(Name = "Authorization")] string token, [FromBody] MenuDTO input)
        {
            if(token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _menuService.Create(token, input);
        }

        [HttpPut]
        public async Task<object> Update([FromHeader(Name = "Authorization")] string token, [FromBody] MenuDTO input)
        {
            if (token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _menuService.Update(token, input);
        }

        [HttpDelete]
        public async Task<ResultDTO> Delete([FromHeader(Name = "Authorization")] string token, [FromBody] int input)
        {
            if (token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _menuService.Delete(token, input);
        }
    }
}
