using System;
using System.Linq;
using Bank.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Bank.Data
{
    public class DatabaseInitalizer
    {
        public void Initialize(
            BankAppDataContext context,
            ILogger<DatabaseInitalizer> logger,
            RoleManager<IdentityRole> roleManager,
            UserManager<BankUser> userManager)
        {
            MigrateChanges(context, logger);
            SeedRoles(roleManager);
            SeedUsers(userManager);
            SeedData(context);
        }
        private void MigrateChanges(BankAppDataContext context, ILogger<DatabaseInitalizer> logger)
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

        private void SeedData(BankAppDataContext context)
        {
            if (context.Customers.FirstOrDefault(x => x.Username == "TestApiUser") == null)
            {
                var testApiCustomer = new Customers()
                {
                    Username = "TestApiUser",
                    Password = "Hejsan123#",
                    Birthday = new DateTime(1980, 08, 16),
                    City = "Stockholm",
                    Country = "Sweden",
                    Emailaddress = "test@api.se",
                    UniqueId = "03ECF1B1-D2DC-499B-B9A0-46890F705DA9",
                    CountryCode = "SE",
                    Gender = "Male",
                    Givenname = "Test",
                    Surname = "Testsson",
                    Streetaddress = "testgatan",
                    Zipcode = "16762",
                    Telephonecountrycode = "46",
                    Telephonenumber = "0702675432"
                  
                };
                context.Customers.Add(testApiCustomer);               

                Accounts newTestAccount = new Accounts() { Balance = 100, Created = DateTime.Today, Frequency = "Monthly" };
                context.Accounts.Add(newTestAccount);

                context.SaveChanges();

                Dispositions newDisposition = new Dispositions()
                { AccountId = newTestAccount.AccountId, CustomerId = testApiCustomer.CustomerId, Type = "OWNER" };

                context.Dispositions.Add(newDisposition);
                context.SaveChanges();
            }
        }
    }
}
