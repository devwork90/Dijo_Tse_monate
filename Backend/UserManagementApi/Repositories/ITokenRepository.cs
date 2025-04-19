using Microsoft.AspNetCore.Identity;

namespace UserManagementApi.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);

    }
}
