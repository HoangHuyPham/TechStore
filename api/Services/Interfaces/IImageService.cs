namespace api.Services.Interfaces
{
    public interface IImageService
    {
        Task<string?> Upload(IFormFile file);
    }
}