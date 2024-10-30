using AuthService.DTOs;
using Business.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;

        public AuthController(IAuthenticationService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _authService.Login(request.Username, request.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(new { Token = user.Token });
        }

        [HttpPost("login/facebook")]
        public async Task<IActionResult> LoginWithFacebook([FromBody] ProviderLoginRequest request)
        {
            var user = await _authService.LoginWithFacebook(request.ProviderIdStr);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(new { Token = user.Token });
        }

        [HttpPost("login/google")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] ProviderLoginRequest request)
        {
            var user = await _authService.LoginWithGoogle(request.ProviderIdStr);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(new { Token = user.Token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel request)
        {
            var user = new UserInfo();
            user.Username = request.Username;
            user.Email = request.Email;
            user.PasswordHash = request.PasswordHash;
            await _authService.Register(user);
            return Ok();
        }

        [Authorize]
        [HttpGet("protected")]
        public IActionResult Protected()
        {
            return Ok("This is a protected endpoint");
        }
    }
}
