using api.DTOs.Preview;
using api.Models;

namespace api.Mappers
{
    public static class PreviewMapper
    {
        public static Preview ParseToPreview(this PreviewCreateDTO createDTO){
            return new Preview{
                URL = createDTO.URL,
            };
        }
    }
}