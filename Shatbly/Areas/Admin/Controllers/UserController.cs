using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shatbly.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    [Authorize(Roles = $"{SD.ROLE_SUPER_ADMIN},{SD.ROLE_ADMIN}")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index(string? name, int page = 1)
        {
            var users = _userManager.Users.AsNoTracking();

            if (name is not null)
                users = users.Where(e => e.UserName.Contains(name));

            if (page < 1)
                page = 1;
            int pageSize = 10;
            int currentPage = page;
            double totalCount = Math.Ceiling(users.Count() / (double)pageSize);
            users = users.Skip((page - 1) * pageSize).Take(pageSize);
            var usersList = await users.ToListAsync();
            var model = new List<UserWithRoleVM>();
            foreach (var user in usersList)
            {
                var roles = await _userManager.GetRolesAsync(user);

                model.Add(new UserWithRoleVM
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    RoleName = roles.FirstOrDefault()
                });
            }




            return View(new UsersVM
            {
                Users = model,
                TotalPages = totalCount,
                CurrentPage = currentPage
            });
        }
        [HttpGet]
        public IActionResult Create()
        {

            var roles = _roleManager.Roles.AsNoTracking().AsQueryable();

            return View(new CreateUserVM
            {
                Roles = roles.AsEnumerable()
            });
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserVM createUserVM)
        {
            ModelState.Remove("Id");
            ModelState.Remove("User");
            ModelState.Remove("Roles");
            if (!ModelState.IsValid)
            {
                TempData["error-notification"] = "Invalid Data";
                createUserVM.Roles = _roleManager.Roles.AsNoTracking().ToList();

                return View(createUserVM);
            }
            var user = new User
            {
                UserName = createUserVM.UserName,
                Email = createUserVM.Email,
            };
            var result = await _userManager.CreateAsync(user, createUserVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                TempData["error-notification"] = $"Save Failed";
            }
            else
            {
                await _userManager.AddToRoleAsync(user, createUserVM.RoleName);
                TempData["success-notification"] = $"Save Successful";
            }


            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var roles = _roleManager.Roles.AsNoTracking().AsQueryable();
            var userRoles = await _userManager.GetRolesAsync(user);
            return View(new EditUserVM
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                RoleName = userRoles.FirstOrDefault(),
                Roles = roles.AsEnumerable()
            });
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserVM editUserVM)
        {
            ModelState.Remove("Id");
            ModelState.Remove("User");
            ModelState.Remove("Roles");
            if (!ModelState.IsValid)
            {
                TempData["error-notification"] = "Invalid Data";
                var roles = _roleManager.Roles.AsNoTracking().AsQueryable();
                editUserVM.Roles = roles.AsEnumerable();
                return View(editUserVM);
            }
            var user = await _userManager.FindByIdAsync(editUserVM.Id);
            if (user == null)
            {
                return NotFound();
            }
            user.UserName = editUserVM.UserName;
            user.Email = editUserVM.Email;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                TempData["error-notification"] = $"Update Failed";
            }
            else
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRoleAsync(user, editUserVM.RoleName);
                TempData["success-notification"] = $"Update Successful";
            }
            return RedirectToAction(nameof(Index));

        }
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                TempData["error-notification"] = $"Delete Failed";
            }
            else
            {
                TempData["success-notification"] = $"Delete Successful";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
