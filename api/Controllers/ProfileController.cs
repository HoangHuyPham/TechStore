using System.Security.Claims;
using api.DTOs.User;
using api.Mappers;
using api.Models;
using api.Repos.Interfaces;
using api.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController(IUserRepository repoUser) : ControllerBase
    {
        private readonly IUserRepository _repoUser = repoUser;
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            var existUser = await _repoUser.FindById(Guid.Parse(userId!));

            if (existUser == null)
            {
                return Ok(new APIResponse<string>
                {
                    Status = 10000,
                    Data = "Invalid user."
                });
            }

            return Ok(new APIResponse<UserDTO>
            {
                Status = 200,
                Data = existUser.ParseToDTO()
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileUpdateDTO updateDTO)
        {
            var userId = HttpContext.User.FindFirstValue("Id");
            if (userId == null) return BadRequest();
            var existUser = await _repoUser.FindById(Guid.Parse(userId));

            if (existUser == null)
            {
                return Ok(new APIResponse<string>
                {
                    Status = 10000,
                    Data = "User is unavailable."
                });
            }

            existUser.Name = updateDTO.Name;
            existUser.AvatarId = updateDTO.AvatarId;
            existUser.Address = updateDTO.Address;
            existUser.Phone = updateDTO.Phone;

            await _repoUser.Update(Guid.Parse(userId), existUser);

            var res = await _repoUser.FindById(Guid.Parse(userId));
            if (res == null)
            {
                return Ok(new APIResponse<string>
                {
                    Status = 10001,
                    Data = "Can't fetch user"
                });
            }

            return Ok(new APIResponse<UserDTO>
            {
                Status = 200,
                Data = res.ParseToDTO()
            });
        }
    }
}