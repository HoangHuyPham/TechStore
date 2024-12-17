using api.DTOs.Product;
using api.Mappers;
using api.Models;
using api.Repos.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(IRepository<Category> repository) : ControllerBase
    {
        private readonly IRepository<Category> _repository = repository;
        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var categories = await _repository.FindAll();
            return Ok(categories.Select(x=>x.ParseToDTO()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id){
            var category = await _repository.FindById(id);
            if (category == null) return NotFound();
            return Ok(category.ParseToDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDTO createDTO){
            var category = await _repository.Create(createDTO.ParseToCategory());
            if (category == null) return NotFound();
            return CreatedAtAction(nameof(Get), new { id = category.Id }, category.ParseToDTO());
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryDTO updateDTO){
            var category = await _repository.Update(updateDTO.Id, updateDTO.ParseToCategory());
            if (category == null) return NotFound();
            return Ok(category.ParseToDTO());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id){
            var category = await _repository.Delete(id);
            if (category == null) return NotFound();
            return Ok();
        }
    }
}