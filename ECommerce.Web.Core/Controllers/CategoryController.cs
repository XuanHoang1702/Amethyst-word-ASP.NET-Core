using Admin.DTO;
using Category.DTO;
using Category.Intfs;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public List<CategoryDTO> GetList() => _categoryService.GetList();

        [HttpPost]
        public async Task<object> Create([FromHeader(Name = "Authorization")] string token, [FromBody] CategoryCreateDTO input)
        {
            if(token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _categoryService.Create(token, input);
        }

        [HttpPut]
        public async Task<object> Update([FromHeader(Name = "Authorization")] string token, [FromBody] CategoryUpdateDTO input)
        {
            if (token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _categoryService.Update(token, input);

        }

        [HttpDelete]
        public async Task<ResultDTO> Delete([FromHeader(Name = "Authorization")] string token, [FromBody] int input)
        {
            if(token.StartsWith("Bearer "))
                token = token.Substring(7);
            return await _categoryService.Delete(token, input);
        }
    }
}
