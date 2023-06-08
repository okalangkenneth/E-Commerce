using E_Commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Data
{
    public class DataSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<DataSeeder> _logger;

        public DataSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<DataSeeder> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task SeedData()
        {
            var adminRole = new IdentityRole("Admin");
            if (!await _roleManager.RoleExistsAsync(adminRole.Name))
            {
                var adminRoleResult = await _roleManager.CreateAsync(adminRole);
                if (!adminRoleResult.Succeeded)
                {
                    _logger.LogError($"Error creating Admin role: {string.Join(", ", adminRoleResult.Errors.Select(x => x.Description))}");
                }
            }

            var customerRole = new IdentityRole("Customer");
            if (!await _roleManager.RoleExistsAsync(customerRole.Name))
            {
                var customerRoleResult = await _roleManager.CreateAsync(customerRole);
                if (!customerRoleResult.Succeeded)
                {
                    _logger.LogError($"Error creating Customer role: {string.Join(", ", customerRoleResult.Errors.Select(x => x.Description))}");
                }
            }

            var adminUser = new ApplicationUser
            {
                Email = "admin@ecommerce.com",
                UserName = "admin@ecommerce.com",
                EmailConfirmed = true,
            };

            var user = await _userManager.FindByEmailAsync(adminUser.Email);
            if (user == null)
            {
                var result = await _userManager.CreateAsync(adminUser, "DefaultAdminPassword");
                if (result.Succeeded)
                {
                    var createdUser = await _userManager.FindByEmailAsync(adminUser.Email);
                    if (createdUser != null)
                    {
                        var addToRoleResult = await _userManager.AddToRoleAsync(createdUser, adminRole.Name);
                        if (!addToRoleResult.Succeeded)
                        {
                            _logger.LogError($"Error adding role: {string.Join(", ", addToRoleResult.Errors.Select(x => x.Description))}");
                        }
                    }
                    else
                    {
                        _logger.LogError("Created user couldn't be found");
                    }
                }
                else
                {
                    _logger.LogError($"Error creating user: {string.Join(", ", result.Errors.Select(x => x.Description))}");
                }
            }
        }
    }
}


