using Application.Share;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UploadImageController : Controller
    {
        private readonly IFunction _function;
        public UploadImageController(IFunction function)
        {
            _function = function;
        }

        [HttpPost]
        public string Upload() => _function.OpenImgur();
    }
}
