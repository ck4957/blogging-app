using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthDbContext: IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "af4b11c7-4f87-49a6-b98c-328a2150ad7c";
            var writerRoleId = "fa330f43-31ae-4e18-9a1d-52b94ad1891b";
            // Create Reader and Writer role
            var roles = new List<IdentityRole>
            {
                new IdentityRole(readerRoleId)
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp =  readerRoleId
                },
                new IdentityRole(writerRoleId)
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp =  writerRoleId
                }
            };
            // Seed the roles
            builder.Entity<IdentityRole>().HasData(roles);

            // Create an Admin User
            var adminUserId = "02739468-a037-4c29-852d-d3e63fb46593";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            builder.Entity<IdentityUser>().HasData(admin);

            // Give Roles to Admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleId
                },
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
