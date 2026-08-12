using UserManagementApi.Models.DTO;

namespace UserManagementApi.Common.Helpers
{
    public class UserAuthenticationResults
    {
        public bool IsAuthenticated { get; set; }

        public LoginResponseDto? LoginResponse { get; set; }

        public string? Error { get; set; }

        public static UserAuthenticationResults Failure(string error)
        {
            return new UserAuthenticationResults
            {
                IsAuthenticated = false,
                Error = error
            };
        }

        public static UserAuthenticationResults Success(LoginResponseDto loginResponse)
        {
            return new UserAuthenticationResults
            {
                IsAuthenticated = true,
                LoginResponse = loginResponse
            };
        }
    }
}
