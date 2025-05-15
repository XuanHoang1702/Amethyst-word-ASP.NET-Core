using Admin.DTO;
using Brand.DTO;
using Brand.Intfs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public List<BrandDTO> GetList() => _brandService.GetList();

        [HttpPost]
        public async Task<object> Create([FromHeader(Name = "Authorization")] string token, [FromBody] BrandCreateDTO input)
        {
            if(token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _brandService.Create(token, input);
        }

        [HttpPut]
        public async Task<object>Update([FromHeader(Name = "Authorization")] string token, [FromBody] BrandUpdateDTO input)
        {
            if (token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _brandService.Update(token, input);
        }

        [HttpDelete]
        public async Task<ResultDTO> Delete([FromHeader(Name = "Authorization")] string tokenm, [FromBody] int id)
        {
            if(tokenm.StartsWith("Bearer "))
                tokenm = tokenm.Substring(7);
            return await _brandService.Delete(tokenm, id);
        }
    }
}
