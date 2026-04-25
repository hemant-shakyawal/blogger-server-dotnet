using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace blog_api.Data
{
    public class AuthDBContext:IdentityDbContext
    {

        public AuthDBContext(DbContextOptions<AuthDBContext> options):base(options) { 
        
        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "6404f03e-90e4-45e6-9ea1-f77ef0619504";
            var writerRoleId = "daa91357-5472-4e1a-aa1c-c1d9c29bab9d";

            //create reader and writer role

            var roles = new List<IdentityRole>
            {

                new IdentityRole()
                {
                    Id=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper(),
                    ConcurrencyStamp=readerRoleId
                },
                new IdentityRole() {
                Id=writerRoleId,
                Name="Writer",
                 NormalizedName="Writer".ToUpper(),
                  ConcurrencyStamp=writerRoleId

                }
                };



            //seed the role

            builder.Entity<IdentityRole>().HasData(roles);


            //create an Admin User

            var adminUserId = "d30574ac-2a0a-4f28-ab3f-822707a926e9";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                NormalizedEmail = "ADMIN@GMAIL.COM",

                SecurityStamp = adminUserId,

                ConcurrencyStamp = adminUserId


            };


            //  admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");
            admin.PasswordHash = "AQAAAAIAAYagAAAAEGdPmrBkQhTSWyTjeM9W3Js8O2sOW/Qzdce7RY7nGIDpKB+TfTbATeB55bQVYwPLCA==";
            builder.Entity<IdentityUser>().HasData(admin);

            //Give roles to Admin


            var adminRoles = new List<IdentityUserRole<string>>()
            {


                new()
                {
                    UserId=adminUserId,
                    RoleId=readerRoleId
                },
                  new()
                {
                    UserId=adminUserId,
                    RoleId=writerRoleId
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }

    }
}
