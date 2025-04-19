using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserManagementApi.Data
{
    public class DijoDbAuthContext : IdentityDbContext
    {
        public DijoDbAuthContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "cd934d6f-8f57-43ee-8b59-67bdc7e0a049";
            var customerRoleId = "536c7bb0-c18f-42a9-a651-741abd793847";
            var employeeRoleId = "006ad73c-7d3f-4469-a387-d709777f9486";
            var driverRoleId = "def3ad73-f8fa-43ac-ad6b-032ccd0481d6";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                },

                new IdentityRole
                {
                    Id = customerRoleId,
                    ConcurrencyStamp = customerRoleId,
                    Name = "Customer",
                    NormalizedName = "Customer".ToUpper()
                },

                new IdentityRole
                {
                    Id = employeeRoleId,
                    ConcurrencyStamp = employeeRoleId,
                    Name = "Employee",
                    NormalizedName = "Employee".ToUpper()
                },

                new IdentityRole
                {
                    Id = driverRoleId,
                    ConcurrencyStamp = driverRoleId,
                    Name = "Driver",
                    NormalizedName = "Driver".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
