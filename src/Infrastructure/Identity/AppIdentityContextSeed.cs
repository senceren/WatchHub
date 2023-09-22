using ApplicationCore.Constants;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public static class AppIdentityContextSeed
    {
        public static async Task SeedAsync(AppIdentityContext db, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            await db.Database.MigrateAsync(); // otomatik migrasyon

            if (await userManager.Users.AnyAsync() || await roleManager.Roles.AnyAsync())
                return;

            var demoUser = new ApplicationUser()
            {
                UserName = AuthorizationConstant.DEFAULT_DEMO_USER,
                Email = AuthorizationConstant.DEFAULT_DEMO_USER,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(demoUser, AuthorizationConstant.DEFAULT_PASSWORD);

            await roleManager.CreateAsync(new IdentityRole(AuthorizationConstant.Roles.ADMINISTRATOR));

            var adminUser = new ApplicationUser()
            {
                UserName = AuthorizationConstant.DEFAULT_ADMIN_USER,
                Email = AuthorizationConstant.DEFAULT_ADMIN_USER,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(adminUser, AuthorizationConstant.DEFAULT_PASSWORD);
            await userManager.AddToRoleAsync(adminUser,AuthorizationConstant.Roles.ADMINISTRATOR);
        }
    }
}
