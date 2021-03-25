using MASLOW.Entities.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Data
{
    public static class IdentityDataInitializer
    {
        public static void SeedData(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedRoles(RoleManager<Role> roleManager)
        {
            var roles = new[] { "Admin", "User" };

            foreach(var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                {
                    Role r = new Role();
                    r.Name = role;
                    roleManager.CreateAsync(r);
                }
            }
        }

        public static void SeedUsers(UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new[]{
                    "admin", "user"};
                foreach (var user in users)
                {
                    User u = new User();
                    u.Email = $"{user}@maslow.com";
                    u.UserName = user;

                    var password = user;

                    IdentityResult result = userManager.CreateAsync(u, password).Result;

                    if (result.Succeeded)
                    {
                        var role = user;
                        userManager.AddToRoleAsync(u, user).Wait();
                    }
                }
            }
        }
    }
}
