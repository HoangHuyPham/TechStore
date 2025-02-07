using api.DTOs.Product;
using api.Mappers;
using api.Models;
using api.Repos.Interfaces;
using api.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize(Roles = "c26b7fcb-9e16-47aa-893e-3ef148de9714")] // Require admin
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserRepository UserRepo) : ControllerBase
    {
        private readonly IUserRepository _UserRepo = UserRepo;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _UserRepo.FindAll();
            return Ok(new APIResponse<IEnumerable<User>>{
                Status = 200,
                Data = users
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var User = await _UserRepo.FindById(id);
            if (User == null) return NotFound();
            return Ok(new APIResponse<User>{
                Status = 200,
                Data = User
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] User createDTO)
        {
            var User = await _UserRepo.Create(createDTO);
            if (User == null) return NotFound();
            return CreatedAtAction(nameof(Get), new { id = User.Id }, User.ParseToDTO());
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] User updateDTO)
        {
            var User = await _UserRepo.Update(updateDTO.Id, updateDTO);
            if (User == null) return NotFound();
            return Ok(User.ParseToDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var User = await _UserRepo.Delete(id);
            if (User == null) return Ok(new APIResponse<string>{
                Status = 10000,
                Data = "Not found user."
            });
            return Ok(new APIResponse<string>{
                Status = 200,
                Data = "Removed user."
            });
        }
    }
}