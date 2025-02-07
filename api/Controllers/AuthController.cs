using System.Security.Claims;
using api.DTOs.Auth;
using api.Responses;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var existUser = await _authService.Login(loginDTO);
            if (existUser == null) return Ok(new APIResponse<string>
            {
                Status = 10000,
                Data = "Username or password is incorrect."
            });
            var jwtToken = _authService.generateToken(existUser);
            if (jwtToken == null) return Ok(new APIResponse<string>
            {
                Status = 10001,
                Data = "Generate jwt token failed."
            });
            return Ok(new APIResponse<string>
            {
                Status = 200,
                Data = jwtToken
            });
        }

        [HttpPost("CheckEmail")]
        public async Task<IActionResult> CheckEmailExist(CheckEmailDTO email)
        {
            var result = await _authService.UserAvailable(email);
            return Ok(new APIResponse<bool>
            {
                Status = 200,
                Data = result
            });
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var userId = HttpContext.User.FindFirstValue("Id");

            if (userId == null)
            {
                return Ok(new APIResponse<string>
                {
                    Status = 10000,
                    Data = "Empty user."
                });
            }

            var result = await _authService.ChangePassword(Guid.Parse(userId), changePasswordDTO);
            
            return Ok(new APIResponse<bool>
            {
                Status = 200,
                Data = result
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var newUser = await _authService.Register(registerDTO);
            if (newUser == null) return BadRequest("Register failed.");
            return Ok(new APIResponse<string>
            {
                Status = 200,
                Data = "Register success"
            });
        }
    }
}