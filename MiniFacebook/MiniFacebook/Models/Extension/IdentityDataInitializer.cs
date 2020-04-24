using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniFacebook.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFacebook.Extension
{
    public static class IdentityDataInitializer
    {
        public static void seed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            seedAdminRole(roleManager);
            seedAdminUser(userManager);
        }
        public static void seedAdminRole(RoleManager<Role> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                Role role = new Role
                {
                    Id = "1",
                    Name = "Admin",
                    Description = "Admininstrator Role For  Perform Creating ,block , unblock User , create Role and Assign Role to users"
                };
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
        public static  void seedAdminUser(UserManager<User> userManager)
        {
            if ( userManager.FindByEmailAsync("admin@admin.com").Result == null)
            {
                User user = new User
                {
                    Id = "1",
                    UserName= "admin@admin.com",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                    Gender = 0,
                    BirthDate = new DateTime(1995, 1, 1),
                    PhoneNumberConfirmed=false,
                    TwoFactorEnabled=false,
                    LockoutEnabled=false,
                    AccessFailedCount=0
                   
                };
                IdentityResult result = userManager.CreateAsync(user, "Admin123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
