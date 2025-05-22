using Microsoft.AspNetCore.Identity;

namespace UserManagementApi.Models.Domain
{
    public class ExtendedUser: IdentityUser
    {
        public Guid? RestaurantId { get; set; }
   
    }
}
