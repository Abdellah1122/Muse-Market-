using FananeenAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FananeenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageCompareController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ImageCompareController(IWebHostEnvironment env)
        {
            _env = env;
        }

            [HttpPost]
            public async Task<IActionResult> CompareImage(IFormFile file)
            {
                if (file == null || file.Length == 0)
                    return BadRequest("No image uploaded.");

                // Save uploaded image temporarily
                var tempPath = Path.GetTempFileName();
                using (var stream = System.IO.File.Create(tempPath))
                {
                    await file.CopyToAsync(stream);
                }

                // Point to wwwroot/ImagesOeuvre
                string folderPath = Path.Combine(_env.WebRootPath, "ImagesOeuvre");

                try
                {
                    var closest = ImageComparer.FindClosestImage(tempPath, folderPath);
                    return Ok(new { closestImage = closest });
                }
                finally
                {
                    System.IO.File.Delete(tempPath); // clean up
                }
            }
    }
}
