using api.DTOs;
using api.DTOs.Product;
using api.Mappers;
using api.Models;
using api.Query;
using api.Repos.Interfaces;
using api.Responses;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductRepository productRepo, IProductService productService) : ControllerBase
    {
        private readonly IProductRepository _productRepo = productRepo;
        private readonly IProductService _productService = productService;
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductQuery query)
        {
            if (query.Page < 1 && query.PageSize < 0) return BadRequest();
            var products = await _productRepo.FindAll(query);
            return Ok(new APIResponse<PaginationResponseDTO<Product>>
            {
                Status = 200,
                Data = products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var product = await _productRepo.FindById(id);
            if (product == null) return NotFound();
            return Ok(new APIResponse<Product>
            {
                Status = 200,
                Data = product
            });
        }

        [Authorize(Roles = "c26b7fcb-9e16-47aa-893e-3ef148de9714")] // Require admin
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO createDTO)
        {
            var product = await _productService.Create(createDTO);
            if (product == null) return Ok(new APIResponse<string>
            {
                Status = 10000,
                Data = "Add product failed."
            });
            return Ok(new APIResponse<Product>
            {
                Status = 200,
                Data = product
            });
        }

        [Authorize(Roles = "c26b7fcb-9e16-47aa-893e-3ef148de9714")] // Require admin
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateDTO updateDTO)
        {
            var existProduct = await _productRepo.FindById(updateDTO.Id);
            if (existProduct == null) return Ok(new APIResponse<string>
            {
                Status = 10000,
                Data = "Not found product."
            });

            var product = await _productRepo.Update(updateDTO.Id, existProduct);
            if (product == null) return Ok(new APIResponse<string>
            {
                Status = 10001,
                Data = "Update product failed."
            });

            return Ok(new APIResponse<Product>
            {
                Status = 200,
                Data = product
            });
        }

        [Authorize(Roles = "c26b7fcb-9e16-47aa-893e-3ef148de9714")] // Require admin
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var product = await _productRepo.Delete(id);
            if (product == null) return Ok(new APIResponse<string>
            {
                Status = 10000,
                Data = "Delete failed."
            });
            return Ok(new APIResponse<string>
            {
                Status = 200,
                Data = "Deleted product."
            });
        }
    }
}