using api.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace api.Services
{
    public class ImageService(IConfiguration configuration) : IImageService
    {
        private readonly IConfiguration _configuration = configuration;
        public async Task<string?> Upload(IFormFile file)
        {
            var hashName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var path = _configuration.GetSection("FileStorage:Path")!.Value;
            if (path == null)
                return null;
            using FileStream stream = new FileStream(Path.Combine(path, hashName), FileMode.Create);
            await file.CopyToAsync(stream);
            return hashName;
        }
    }
}