using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public UploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            if (_environment.WebRootPath == null)
                return StatusCode(500, "WebRootPath is null.");

            string uploadPath = Path.Combine(_environment.WebRootPath, "images");

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string imageUrl = $"{fileName}";
            return Ok(new { imageUrl });
        }
    }
}
