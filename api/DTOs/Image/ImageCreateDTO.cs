using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Image
{
    public class ImageCreateDTO
    {
        public string? Name { get; set; }
        public string?  Url { get; set; }
    }
}