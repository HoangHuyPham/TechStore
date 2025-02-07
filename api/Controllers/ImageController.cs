using api.DTOs.Image;
using api.Models;
using api.Repos.Interfaces;
using api.Responses;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController(IImageService imageService, IRepository<Image> imageRepo) : ControllerBase
    {
        private readonly IImageService _imageService = imageService;
        private readonly IRepository<Image> _imageRepo = imageRepo;

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            // Check type (.jpg, .png)
            List<string> acceptType = [".jpg", ".png"];

            if (!acceptType.Contains(Path.GetExtension(file.FileName)))
                return Ok(new APIResponse<string>
                {
                    Status = 10000,
                    Data = "Only support .png, .jpg"
                });

            // Check Size (5mb)
            if (file.Length > 1024 * 1024 * 5)
            {
                return Ok(new APIResponse<string>
                {
                    Status = 10001,
                    Data = "File is exceed size limit (5mb)."
                });
            }


            var result = await _imageService.Upload(file);
            if (result == null)
            {
                return Ok(new APIResponse<string>
                {
                    Status = 10002,
                    Data = "Upload failed."
                });
            }

            var image = await _imageRepo.Create(new Image
            {
                Name = DateTime.Now.ToString(),
                Url = "https://localhost:5000/static/" + result
            });

            if (image == null)
            {
                return Ok(new APIResponse<string>
                {
                    Status = 10003,
                    Data = "Create failed."
                });
            }

            return Ok(new APIResponse<Image>
            {
                Status = 200,
                Data = image
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ImageCreateDTO createDTO)
        {
            var image = await _imageRepo.Create(new Image
            {
                Name = createDTO.Name,
                Url = createDTO.Url
            });

            if (image == null)
            {
                return Ok(new APIResponse<string>
                {
                    Status = 10000,
                    Data = "Create failed."
                });
            }

            return Ok(new APIResponse<Image>
            {
                Status = 200,
                Data = image
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return Ok(new APIResponse<Image>
            {
                Status = 200,
                Data = await _imageRepo.Delete(id)
            });
        }
    }
}
