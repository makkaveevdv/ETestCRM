using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETestCRM.Models
{
    public class IdentitySeedData
    {
        private const string adminUserName = "Superadmin";
        private const string adminPassword = "Password77!";
        private const string email = "makkaveevdv@yandex.ru";
        private const string role = "Superadmin";

        public static async Task CreateSuperAdminAccount(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {           

            if (await userManager.FindByNameAsync(adminUserName) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
                AppUser user = new AppUser
                {
                    UserName = adminUserName,
                    Email = email
                };
                IdentityResult result = await userManager.CreateAsync(user, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}
