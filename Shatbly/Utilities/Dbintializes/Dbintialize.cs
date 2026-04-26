<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
=======
﻿using Microsoft.IdentityModel.Tokens;
>>>>>>> 25e986e37d57eddf52a865035ac65f84831b4598
namespace Shatbly.Utilities.Dbintializes
{
    public class Dbintialize : IDbintialize
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;

        public Dbintialize(RoleManager<IdentityRole> roleManager ,UserManager<User> userManager,
            ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }
    public async Task Intializer()
        {
            if(_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }
            if (_roleManager.Roles.IsNullOrEmpty())
            {
                await _roleManager.CreateAsync(new(SD.ROLE_SUPER_ADMIN));
                await _roleManager.CreateAsync(new(SD.ROLE_ADMIN));
                await _roleManager.CreateAsync(new(SD.ROLE_WORKER));
                await _roleManager.CreateAsync(new(SD.ROLE_CUSTOMER));

                await _userManager.CreateAsync(new()
                {
                    FName = "Super",
                    LName = "Admin",
                    Email = "SuperAdmin@gmail.com",
                    EmailConfirmed = true,
                    Phone = "01222222222",
                    UserName = "SuperAdmin"
                }, "Super123@");
                await _userManager.CreateAsync(new()
                {
                    FName = "Admin",
                    LName = "1",
                    Email = "Admin@gmail.com",
                    EmailConfirmed = true,
                    Phone = "01555555555",  
                    UserName = "Admin"
                }, "Admin123@");
                await _userManager.CreateAsync(new()
                {
                    FName = "Worker",
                    LName = "1",
                    Email = "Worker@gmail.com",
                    EmailConfirmed = true,
                    Phone = "01111111111",
                    UserName = "Worker"
                }, "Worker123@");
                await _userManager.CreateAsync(new()
                {
                    FName = "Customer",
                    LName = "1",
                    Email = "Customer@gmail.com",
                    EmailConfirmed = true,
                    Phone = "01000000000",
                    UserName = "Customer"
                },"Customer123@");
                var user = await _userManager.FindByNameAsync("SuperAdmin");
                var user2 = await _userManager.FindByNameAsync("Admin");
                var user3 = await _userManager.FindByNameAsync("Worker");
                var user4 = await _userManager.FindByNameAsync("Customer");
                if (user is not null && user2 is not null&& user3 is not null && user4 is not null) 
                {
                    await _userManager.AddToRoleAsync(user , SD.ROLE_SUPER_ADMIN);
                    await _userManager.AddToRoleAsync(user2 , SD.ROLE_ADMIN);
                    await _userManager.AddToRoleAsync(user3 , SD.ROLE_WORKER);
                    await _userManager.AddToRoleAsync(user4 , SD.ROLE_CUSTOMER);
                }
            }

        }
    }
}
