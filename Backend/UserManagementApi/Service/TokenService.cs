using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserManagementApi.Common.Helpers;
using UserManagementApi.Models.Domain;
using UserManagementApi.Models.DTO;
using UserManagementApi.Repositories;

namespace UserManagementApi.Service
{
   
    public class TokenService :ITokenService
    {
        private readonly UserManager<ExtendedUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public TokenService(UserManager<ExtendedUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        public async Task<UserAuthenticationResults> UserAuthenticationAsync(LoginRequestDto loginRequestDto)
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
                        return UserAuthenticationResults.Success(token);
                    }
                }
            }


            return UserAuthenticationResults.Failure("Username/Password is incorrect");
        }

        public async Task<UserRegistrationResults> UserRegistrationAsync(UserRegisterRequestDto userRegisterRequestDto)
        {
            try
            {
                // Basic Validation
                if (userRegisterRequestDto.Roles == null || !userRegisterRequestDto.Roles.Any())
                {
                    return UserRegistrationResults.Failure("At least one role must be assigned.");
                }

                var requiredRestaurantId = userRegisterRequestDto.Roles.Any(role =>
                    role.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||
                    role.Equals("Employee", StringComparison.OrdinalIgnoreCase));

                if (requiredRestaurantId && userRegisterRequestDto.RestaurantId == null)
                    return UserRegistrationResults.Failure("RestaurantId is required for Admin and Employee roles.");

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
                    return UserRegistrationResults.Failure($"User creation failed: {string.Join("; ", errors)}");
                }

                if (userRegisterRequestDto.Password != null && userRegisterRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, userRegisterRequestDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return UserRegistrationResults.Success(new UserRegistrationResponseDto
                        {
                            Message = "User registered successfully with the relevant roles."
                        });
                    }
                    else
                    {
                        var errors = identityResult.Errors.Select(e => e.Description);
                        return UserRegistrationResults.Failure($"Failed to assign roles: {string.Join("; ", errors)}");
                    }
                }

                return UserRegistrationResults.Failure("Something went wrong");
            }
            catch (Exception ex)
            {
                return UserRegistrationResults.Failure($"Internal server error: {ex.Message}");
            }
        }
    }
}
