using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bank.Web.Data
{
    public class DatabaseInitalizer
    {
        public void Initialize(
            BankAppDataContext context,
            ILogger<Program> logger,
            RoleManager<IdentityRole> roleManager,
            UserManager<BankUser> userManager)
        {
            MigrateChanges(context, logger);
            SeedRoles(roleManager);
            SeedUsers(userManager);
            //Seed data?
        }
        private void MigrateChanges(BankAppDataContext context, ILogger<Program> logger)
        {
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured during migration");
            }
        }
        private void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                var identityRole = new IdentityRole("Admin");
                IdentityResult roleResult = roleManager.CreateAsync(identityRole).Result;
            }

            if (!roleManager.RoleExistsAsync("Cashier").Result)
            {
                var identityRole = new IdentityRole("Cashier");
                IdentityResult roleResult = roleManager.CreateAsync(identityRole).Result;
            }
        }
        private void SeedUsers(UserManager<BankUser> userManager)
        {
                       
            if (userManager.FindByEmailAsync("stefan.holmberg@systementor.se").Result == null)
            {
                BankUser user = new BankUser();               
                user.UserName = "stefan.holmberg@systementor.se";
                user.Email = "stefan.holmberg@systementor.se";
                
                IdentityResult result = userManager.CreateAsync(user, "Hejsan123#").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Admin").Wait();
                }
            }

            if (userManager.FindByEmailAsync("stefan.holmberg@nackademin.se").Result == null)
            {
                BankUser user = new BankUser();
                user.UserName = "stefan.holmberg@nackademin.se";
                user.Email = "stefan.holmberg@nackademin.se";

                IdentityResult result = userManager.CreateAsync(user, "Hejsan123#").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                                        "Cashier").Wait();
                }
            }
        }
    }
}
