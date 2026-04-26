using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shatbly.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    [Authorize(Roles = $"{SD.ROLE_ADMIN} , {SD.ROLE_SUPER_ADMIN}")]

    public class BanerController : Controller
    {
        private readonly IRepository<Banner> _bannerRepo;

        public BanerController(IRepository<Banner> bannerRepo)
        {
            _bannerRepo = bannerRepo;
        }

        public async Task<IActionResult> Index(string? title, int page = 1)
        {

            var banners = await _bannerRepo.GetAsync(tracking: false);
            //Add new filter
            if (title is not null)
                banners = banners.Where(e => e.Title.Contains(title)).ToList();

            // Pagination
            if (page < 1)
                page = 1;
            int pageSize = 5;
            int currentPage = page;
            double totalCount = Math.Ceiling(banners.Count() / (double)pageSize);
            banners = banners.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(new BannersVM
            {
                Banners = banners.AsEnumerable(),
                CurrentPage = currentPage,
                TotalPages = totalCount
            });
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Banner banner, IFormFile img)
        {
            ModelState.Remove("ImageUrl");
            ModelState.Remove("User");
            ModelState.Remove("UserId");
            if (!ModelState.IsValid)
                return View(banner);
           
                var newFileName = Guid.NewGuid().ToString() + DateTime.UtcNow.ToString("yyyy-MM-dd") + Path.GetExtension(img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\banners", newFileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    img.CopyTo(stream);
                }
                banner.ImageUrl = newFileName;

            await _bannerRepo.CreateAsync(banner);
            await _bannerRepo.CommitAsync();
            TempData["Notification"] = "Banner created successfully";

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Roles = $"{SD.ROLE_ADMIN} , {SD.ROLE_SUPER_ADMIN}")]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var banner = await _bannerRepo.GetOneAsync(e => e.Id == id);
            if (banner is null)
                return NotFound();
            return View(banner);
        }
        [HttpPost]
        [Authorize(Roles = $" {SD.ROLE_SUPER_ADMIN}")]
        public async Task<IActionResult> Edit(Banner banner, IFormFile? img)
        {
            ModelState.Remove("ImageUrl");
            ModelState.Remove("User");
            ModelState.Remove("UserId");
            if (!ModelState.IsValid)
                return View(banner);

            Banner? existingBanner = await _bannerRepo.GetOneAsync(e => e.Id == banner.Id, tracking: false);
            if (existingBanner is null)
                return NotFound();
           
            
                var newFileName = Guid.NewGuid().ToString() + DateTime.UtcNow.ToString("yyyy-MM-dd") + Path.GetExtension(img.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\banners", newFileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    img.CopyTo(stream);
                }
                // Optionally delete the old banner file
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\banners", existingBanner.ImageUrl);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
                banner.ImageUrl = newFileName;

            if (img is  null && img.Length < 0)
            {
                banner.ImageUrl = existingBanner.ImageUrl;
            }
            _bannerRepo.Update(banner);
            await _bannerRepo.CommitAsync();
            TempData["Notification"] = "Banner updated successfully";
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = $" {SD.ROLE_SUPER_ADMIN}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var banner = await _bannerRepo.GetOneAsync(e => e.Id == id);
            if (banner is null)
                return NotFound();
            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\banners", banner.ImageUrl);
            if (System.IO.File.Exists(oldFilePath))
            {
                System.IO.File.Delete(oldFilePath);
            }
            _bannerRepo.Delete(banner);
            await _bannerRepo.CommitAsync();
            TempData["Notification"] = "Banner deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
