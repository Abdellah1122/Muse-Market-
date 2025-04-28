using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FananeenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("upload-imageA")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesArtists");
            var filePath = Path.Combine(uploadPath, file.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Ok(new { FilePath = $"/ImagesArtists/{file.FileName}" });
        }
        [HttpPost("upload-imageO")]
        public async Task<IActionResult> UploadImageOeuvre(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "ImagesOeuvre");
            var filePath = Path.Combine(uploadPath, file.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Ok(new { FilePath = $"/ImagesOeuvre/{file.FileName}" });
        }
        [HttpPost("upload-imageC")]
        public async Task<IActionResult> UploadImageCommision(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "ImageCommision");
            var filePath = Path.Combine(uploadPath, file.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return Ok(new { FilePath = $"/ImageCommision/{file.FileName}" });
        }
    }
}
