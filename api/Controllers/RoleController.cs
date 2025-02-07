using api.DTOs.Product;
using api.Mappers;
using api.Models;
using api.Repos.Interfaces;
using api.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize(Roles = "c26b7fcb-9e16-47aa-893e-3ef148de9714")]
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController(IRepository<Role> RoleRepo) : ControllerBase
    {
        private readonly IRepository<Role> _RoleRepo = RoleRepo;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Roles = await _RoleRepo.FindAll();
            return Ok(new APIResponse<IEnumerable<Role>>{
                Status = 200,
                Data = Roles
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var Role = await _RoleRepo.FindById(id);
            if (Role == null) return NotFound();
            return Ok(new APIResponse<Role>{
                Status = 200,
                Data = Role
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Role createDTO)
        {
            var Role = await _RoleRepo.Create(createDTO);
            if (Role == null) return NotFound();
            return CreatedAtAction(nameof(Get), new { id = Role.Id }, Role);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Role updateDTO)
        {
            var Role = await _RoleRepo.Update(updateDTO.Id, updateDTO);
            if (Role == null) return NotFound();
            return Ok(Role);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var Role = await _RoleRepo.Delete(id);
            if (Role == null) return Ok(new APIResponse<string>{
                Status = 10000,
                Data = "Not found Role."
            });
            return Ok(new APIResponse<string>{
                Status = 200,
                Data = "Removed Role."
            });
        }
    }
}