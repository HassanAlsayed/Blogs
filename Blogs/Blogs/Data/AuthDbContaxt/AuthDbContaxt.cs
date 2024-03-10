using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Data.AuthDbContaxt
{
    public class AuthDbContaxt : IdentityDbContext
    {
        public AuthDbContaxt(DbContextOptions<AuthDbContaxt> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //seed role (user,admin,superAdmin)
           
            var superAdminId = "559e4bde-0011-4a71-8bde-9ff87388b4a8";
            var userId = "7c269750-d636-43ee-b627-11aee89b04a6";

            var roles = new List<IdentityRole>()
            {

                 new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superAdminId,
                    ConcurrencyStamp = superAdminId
                },
                  new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userId,
                    ConcurrencyStamp = userId
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            //seed superAdmin user
            var superId = "4f814554-cb2a-4e81-af4a-0aa28dd8986d";
            var superAdminUser = new IdentityUser
            {
                UserName = "superAdmin",
                Email = "superAdmin@gmail.com",
                NormalizedEmail = "superAdmin@gmail.com".ToUpper(),
                NormalizedUserName = "superAdmin".ToUpper(),
                Id = superId
            };
            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser,"superAdmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);

            //add all roles to superadmin

            var superAdminRole = new List<IdentityUserRole<string>>
            {
                
                new IdentityUserRole<string>
                {
                    RoleId = superAdminId,
                    UserId = superId,

                },
                new IdentityUserRole<string>
                {
                    RoleId = userId,
                    UserId = superId,

                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRole);
        }
    }
}
