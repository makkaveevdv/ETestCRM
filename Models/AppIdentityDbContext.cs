using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETestCRM.Models
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser> 
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options) { }

        //public static async Task CreateSuperAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration)
        //{
        //    UserManager<AppUser> userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
        //    RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        //    string username = configuration["Data:AdminUser:UserName"];
        //    string email = configuration["Data:AdminUser:Email"];
        //    string password = configuration["Data:AdminUser:Password"];
        //    string role = configuration["Data:AdminUser:Role"];
        //    string firstname = configuration["Data:AdminUser:FirstName"];
        //    string middlename = configuration["Data:AdminUser:MiddleName"];
        //    string lastname = configuration["Data:AdminUser:LastName"];

        //    if (await userManager.FindByNameAsync(username) == null)
        //    {
        //        if (await roleManager.FindByNameAsync(role) == null)
        //        {
        //            await roleManager.CreateAsync(new IdentityRole(role));
        //        }
        //        AppUser user = new AppUser
        //        {                    
        //            UserName = username,
        //            Email = email,
        //            FirstName = firstname,
        //            MiddleName = middlename,
        //            LastName = lastname
        //        };
        //        IdentityResult result = await userManager.CreateAsync(user, password);
        //        if (result.Succeeded)
        //        {
        //            await userManager.AddToRoleAsync(user, role);
        //        }
        //    }
        //}
    }
}
