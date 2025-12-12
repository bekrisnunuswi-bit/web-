using FitnessCenter.Models;
using FitnessCenter.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessCenter.DataAccess.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAdminUser(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger? logger = null)
        {
            const string adminRole = StaticDetails.Role_Admin;
            const string adminEmail= StaticDetails.Admin_Email;
            const string adminPassword= StaticDetails.Admin_Password;
            const string userRole = StaticDetails.Role_User;
            if (!roleManager.RoleExistsAsync(adminRole).GetAwaiter().GetResult())
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
            }
            if (!await roleManager.RoleExistsAsync(userRole))
            {
                await roleManager.CreateAsync(new IdentityRole(userRole));
            }
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true 
                };
                var result = userManager.CreateAsync(adminUser, adminPassword).GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    logger?.LogWarning("Failed to create admin: {Errors}", string.Join(", ", result.Errors));
                    return;
                }
                await userManager.AddToRoleAsync(adminUser, adminRole);
                logger?.LogInformation("Admin user created: {Email}", adminEmail);
            }
            else
            {
                if (!await userManager.IsInRoleAsync(adminUser, adminRole))
                    await userManager.AddToRoleAsync(adminUser, adminRole);

                // Reset password to known value 'sau' (works even when a password already exists)
                var token = await userManager.GeneratePasswordResetTokenAsync(adminUser);
                var resetResult = await userManager.ResetPasswordAsync(adminUser, token, adminPassword);
                if (!resetResult.Succeeded)
                    logger?.LogWarning("Failed to reset admin password: {Errors}", string.Join(", ", resetResult.Errors));
                else
                    logger?.LogInformation("Admin password reset to '{Pwd}' for {Email}", "[masked]", adminEmail);
            }
        }
    }
}
