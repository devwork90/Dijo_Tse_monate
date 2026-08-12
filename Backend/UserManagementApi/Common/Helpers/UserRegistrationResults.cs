using UserManagementApi.Models.DTO;

namespace UserManagementApi.Common.Helpers
{
    public class UserRegistrationResults
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
        public UserRegistrationResponseDto? User { get; set; }

        public static UserRegistrationResults Failure(string error)
        {
            return new UserRegistrationResults
            {
                IsSuccess = false,
                Error = error
            };
        }

        public static UserRegistrationResults Success(UserRegistrationResponseDto message)
        {
            return new UserRegistrationResults
            {
                IsSuccess = true,
                User = message
            };
        }
    }
}
