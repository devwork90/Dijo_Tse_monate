using Microsoft.AspNetCore.Identity;
using UserManagementApi.Models.Domain;

namespace UserManagementApi.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(ExtendedUser user, List<string> roles);

    }
}
