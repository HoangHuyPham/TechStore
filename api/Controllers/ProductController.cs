using api.DTOs.Product;
using api.Mappers;
using api.Models;
using api.Repos.Interfaces;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IRepository<Product> repoProduct, IProductService productService) : ControllerBase
    {
        private readonly IRepository<Product> _repoProduct = repoProduct;
        private readonly IProductService _productService = productService;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repoProduct.FindAll();
            return Ok(products.Select(x => x.ParseToDTO()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var product = await _repoProduct.FindById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [Authorize(Roles = "c26b7fcb-9e16-47aa-893e-3ef148de9714")] // Require admin
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO createDTO)
        {
            var product = await _productService.Create(createDTO);
            if (product == null) return NotFound();
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product.ParseToDTO());
        }

        [Authorize(Roles = "c26b7fcb-9e16-47aa-893e-3ef148de9714")] // Require admin
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateDTO updateDTO)
        {
            var product = await _productService.Update(updateDTO);
            if (product == null) return NotFound();
            return Ok(product.ParseToDTO());
        }

        [Authorize(Roles = "c26b7fcb-9e16-47aa-893e-3ef148de9714")] // Require admin
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var product = await _repoProduct.Delete(id);
            if (product == null) return NotFound();
            return Ok();
        }
    }
}