using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using UserManagementApi.Models.Domain;
using UserManagementApi.Models.DTO;
using UserManagementApi.Repositories;

namespace UserManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ExtendedUser> userManager;
        private readonly ITokenRepository tokenRepository;
        public AuthController(UserManager<ExtendedUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequestDto userRegisterRequestDto)
        {
            //Basic Validation

            if (userRegisterRequestDto.Roles == null || !userRegisterRequestDto.Roles.Any())
            {
                return BadRequest("At least one role must be assigned.");

            }

            var requiredReastaurantId = userRegisterRequestDto.Roles.Any(role =>
            role.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
            role.Equals("Employee", StringComparison.OrdinalIgnoreCase));


            // Enforce RestaurantId only for Admin/Employee
            if (requiredReastaurantId && userRegisterRequestDto.RestaurantId == null)
                return BadRequest("RestaurantId is required for Admin and Employee roles.");


            var identityUser = new ExtendedUser
            {

                Email = userRegisterRequestDto.Username,
                UserName = userRegisterRequestDto.Name,
                PhoneNumber = userRegisterRequestDto.Phonenumber,
                RestaurantId = userRegisterRequestDto.RestaurantId

            };

            var identityResult = await userManager.CreateAsync(identityUser, userRegisterRequestDto.Password);

            if (!identityResult.Succeeded)
            {
                var errors = identityResult.Errors.Select(e => e.Description);
                return BadRequest(new { Message = "User creation failed", Errors = errors });
            }

            // Add roles to this User
            if (userRegisterRequestDto.Password != null && userRegisterRequestDto.Roles.Any())
            {
                identityResult = await userManager.AddToRolesAsync(identityUser, userRegisterRequestDto.Roles);

                if (identityResult.Succeeded)
                {
                    return Ok("User sucessfully registered! Please Login");
                }
                else
                {
                    var errors = identityResult.Errors.Select(e => e.Description);
                    return BadRequest(new { Message = "Failed to assign roles", Errors = errors });
                }
            }

            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {

                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);

                if (checkPasswordResult)
                {
                    //Get Roles for this user 

                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        //Create Token

                        var jwToken = tokenRepository.CreateJWTToken((ExtendedUser)user, roles.ToList());

                        var token = new LoginResponseDto { JwtToken = jwToken };
                        return Ok(token);
                    }
                }
            }


            return BadRequest("Username/Password is incorrect");
        }
    }
}