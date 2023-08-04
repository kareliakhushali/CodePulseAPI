using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePulseAPI.Data
{
    public class AuthDbContext:IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "f79ef4c5-34ec-4eeb-acae-75ec61734558";
            var writerRoleId = "13048c85-f8af-47f3-94f8-480bb751ab2e";
            //creating reader and writer role
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id= readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id=  writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId
                }
            };

            //seed the roles
            builder.Entity<IdentityRole>().HasData(roles);
            // Create an admin user
            var adminUserId = "f5764ae1-ef0b-4c5d-94f5-d5fe3a007b7d";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com",
                NormalizedEmail = "admin@codepulse.com".ToUpper(),
                NormalizedUserName = "admin@codepulse.com".ToUpper()

            };
            //password for admin
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin,"Admin@123");
            builder.Entity<IdentityUser>().HasData(admin);
            //Give roles to admin
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
                }
            };
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }
    }
}
