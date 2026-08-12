using UserManagementApi.Common.Helpers;
using UserManagementApi.Models.DTO;

namespace UserManagementApi.Service
{
    public interface ITokenService
    {
        Task<UserRegistrationResults> UserRegistrationAsync(UserRegisterRequestDto userRegisterRequestDto);

        Task<UserAuthenticationResults> UserAuthenticationAsync(LoginRequestDto loginRequestDto);
    }
}
