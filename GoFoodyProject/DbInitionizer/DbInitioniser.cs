using BL;
using Domains;
using GoFoodyProject.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoFoodyProject.DbInitionizer
{
    public class DbInitioniser : IDbInitioniser
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ConfigContext _dbContext;

        public DbInitioniser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ConfigContext dbContext)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._dbContext = dbContext;
        }

        public void Initionlize()
        {
            try
            {
                if (_dbContext.Database.GetPendingMigrations().Any())
                {
                    _dbContext.Database.Migrate();
                }

                if (!_roleManager.Roles.Any())
                {
                    _roleManager.CreateAsync(new IdentityRole(SD.SuperAdminRole)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.AdminRole)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.UserRole)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(SD.EmployeeRole)).GetAwaiter().GetResult();
                    var adminUser = new ApplicationUser

                    {
                        UserName = "Admin123@gmail.com",
                        FullName = "Mohamed Reda",
                        Email = "Admin123@gmail.com",
                        PhoneNumber="01210199651"
                        
                    };

                    var result = _userManager.CreateAsync(adminUser, "358741enG@").GetAwaiter().GetResult();

                    if (result.Succeeded)
                    {
                        _userManager.AddToRoleAsync(adminUser, SD.SuperAdminRole).GetAwaiter().GetResult();
                    }
                    else
                    {
                        Console.WriteLine(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Initialization failed: {ex.Message}");
            }
        }

    }
}
