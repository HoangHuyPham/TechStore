using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Preview
{
    public class PreviewCreateDTO
    {
        [Url]
        public required string URL { get; set; }
    }
}