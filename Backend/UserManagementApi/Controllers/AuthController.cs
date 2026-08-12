using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Models.DTO;
using UserManagementApi.Service;

namespace UserManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService tokenService;
        public AuthController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestDto userRegisterRequestDto)
        {
            var result = await tokenService.UserRegistrationAsync(userRegisterRequestDto);
            if (result.IsSuccess)
            {
                return Ok(result.User);
            }
            return BadRequest(result.Error);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var result = await tokenService.UserAuthenticationAsync(loginRequestDto);
            if (result.IsAuthenticated)
            {
                return Ok(result.LoginResponse);
            }
            return BadRequest(result.Error);
        }
    }
}