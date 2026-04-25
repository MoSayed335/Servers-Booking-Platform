using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Shatbly.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    //[Authorize(Roles = $"{SD.ROLE_ADMIN},{SD.ROLE_SUPER_ADMIN}")]
    public class CouponController : Controller
    {
        private readonly IRepository<Coupon> _couponRepo;

        public CouponController(IRepository<Coupon> couponRepo)
        {
            _couponRepo = couponRepo;
        }

        public async Task<IActionResult> Index(string? coupon, int Page = 1)
        {
            var coupons = await _couponRepo.GetAsync(tracking: false);

            if (Page < 1)
                Page = 1;

            int currentPage = Page;
            double TotalPages = Math.Ceiling(coupons.Count() / 5.0);//3

            coupons = coupons.Skip((currentPage - 1) * 5).Take(5).ToList();

            ViewBag.CurrentPage = currentPage;
            ViewBag.TotalPages = TotalPages;

            if (coupon is not null)
                coupons = coupons.Where(c => c.Code.Contains(coupon)).ToList();

            return View(new CouponsVM
            {
                Coupons = coupons,
                CurrentPage = currentPage,
                TotalPages = TotalPages
            });
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Coupon coupon)
        {
            if (!ModelState.IsValid)
            {
                TempData["error-notification"] = "Please correct the errors in the form.";
                return View(coupon);
            }
             
            await _couponRepo.CreateAsync(coupon);
            await _couponRepo.CommitAsync();
            TempData["success-notification"] = "Coupon created successfully";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var coupon = await _couponRepo.GetOneAsync(c => c.Id == id, tracking: false);
            if (coupon is null)
            {
                TempData["error-notification"] = "Coupon not found.";
                return NotFound();
            }
            return View(coupon);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Coupon coupon)
        {
            if (!ModelState.IsValid)
            {
                TempData["error-notification"] = "Please correct the errors in the form.";
                return View(coupon);
            }
            _couponRepo.Update(coupon);
            await _couponRepo.CommitAsync();
            TempData["success-notification"] = "Coupon updated successfully";
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var coupon = await _couponRepo.GetOneAsync(c => c.Id == id);
            if (coupon is null)
            {
                TempData["error-notification"] = "Coupon not found.";
                return NotFound();
            }
            _couponRepo.Delete(coupon);
            await _couponRepo.CommitAsync();
            TempData["success-notification"] = "Coupon deleted successfully";
            return Ok();
        }
    }
}