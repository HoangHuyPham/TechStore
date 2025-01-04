using api.DTOs.Product;
using api.Mappers;
using api.Models;
using api.Repos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(IRepository<Category> repository) : ControllerBase
    {
        private readonly IRepository<Category> _repository = repository;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _repository.FindAll();
            return Ok(categories.Select(x => x.ParseToDTO()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var category = await _repository.FindById(id);
            if (category == null) return NotFound();
            return Ok(category.ParseToDTO());
        }

        [Authorize(Roles = "c26b7fcb-9e16-47aa-893e-3ef148de9714")] // Require admin
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryCreateDTO createDTO)
        {
            var category = await _repository.Create(createDTO.ParseToCategory());
            if (category == null) return NotFound();
            return CreatedAtAction(nameof(Get), new { id = category.Id }, category.ParseToDTO());
        }

        [Authorize(Roles = "c26b7fcb-9e16-47aa-893e-3ef148de9714")] // Require admin
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryDTO updateDTO)
        {
            var category = await _repository.Update(updateDTO.Id, updateDTO.ParseToCategory());
            if (category == null) return NotFound();
            return Ok(category.ParseToDTO());
        }

        [Authorize(Roles = "c26b7fcb-9e16-47aa-893e-3ef148de9714")] // Require admin
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var category = await _repository.Delete(id);
            if (category == null) return NotFound();
            return Ok();
        }
    }
}