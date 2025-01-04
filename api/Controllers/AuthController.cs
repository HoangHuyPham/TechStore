using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Auth;
using api.Services.Interfaces;
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
            if (existUser == null) return BadRequest("Login failed.");
            var jwtToken = _authService.generateToken(existUser);
            if (jwtToken == null) return BadRequest("Something went wrong!");
            return Ok(jwtToken);
        }

        [HttpPost("Login/{Email}")]
        public async Task<IActionResult> CheckEmailExist([FromRoute] string Email)
        {
            var result = await _authService.UserAvailable(Email);
            if (result) return BadRequest();
            return Ok();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var newUser = await _authService.Register(registerDTO);
            if (newUser == null) return BadRequest("Register failed.");
            var jwtToken = _authService.generateToken(newUser);
            if (jwtToken == null) return BadRequest("Something went wrong!");
            return Ok(jwtToken);
        }
    }
}