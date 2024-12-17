using api.DTOs.Product;
using api.Mappers;
using api.Models;
using api.Repos.Interfaces;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IRepository<Product> repoProduct, IRepository<ProductDetail> repoProductDetail,
    IRepository<ProductOption> repoProductOption, IRepository<Category> repoCategory, IProductService productService) : ControllerBase
    {
        private readonly IRepository<Product> _repoProduct = repoProduct;
        private readonly IRepository<ProductDetail> _repoProductDetail = repoProductDetail;
        private readonly IRepository<ProductOption> _repoProductOption = repoProductOption;
        private readonly IRepository<Category> _repoCategory = repoCategory;
        private readonly IProductService _productService = productService;
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var products = await _repoProduct.FindAll();
            return Ok(products.Select(x=>x.ParseToDTO()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id){
            var product = await _repoProduct.FindById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO createDTO){
            var product = await _productService.Create(createDTO);
            if (product == null) return NotFound();
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product.ParseToDTO());
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductUpdateDTO updateDTO){
            var product = await _productService.Update(updateDTO);
            if (product == null) return NotFound();
            return Ok(product.ParseToDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id){
            var product = await _repoProduct.Delete(id);
            if (product == null) return NotFound();
            return Ok();
        }
    }
}