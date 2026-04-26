using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shatbly.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    public class BookingController : Controller
    {
        private readonly IRepository<Booking> _bookingRepo;
        private readonly IRepository<WorkerProfile> _workerprofileRepo;
        private readonly IRepository<Address> _addressRepo;
        private readonly IRepository<Coupon> _couponRepo;
        private readonly IRepository<PromotionCode> _promoCodeRepo;
        private readonly IRepository<User> _userRepo;


        public BookingController(IRepository<Booking> bookingRepo, IRepository<WorkerProfile> workerprofileRepo, IRepository<Address> addressRepo, IRepository<Coupon> couponRepo, IRepository<PromotionCode> promoCodeRepo, IRepository<User> userRepo)
        {
            _bookingRepo = bookingRepo;
            _workerprofileRepo = workerprofileRepo;
            _addressRepo = addressRepo;
            _couponRepo = couponRepo;
            _promoCodeRepo = promoCodeRepo;
            _userRepo = userRepo;
        }

        public async Task<IActionResult> Index(string? name, int page = 1)
        {
            IEnumerable<Booking> bookings = (await _bookingRepo.GetAsync(
     includes: new Expression<Func<Booking, object>>[]
     {
        b => b.Address,
        b => b.Client,
        b => b.Worker,
        b => b.Coupon,
        b => b.PromoCode
     },
     tracking: false))
     .OrderByDescending(b => b.CreatedAt);


            if (!string.IsNullOrWhiteSpace(name))
            {
                var search = name.Trim();

                bookings = bookings.Where(b =>
                    (b.Client != null && b.Client.UserName != null && b.Client.UserName.Contains(search)) ||
                    (b.Coupon != null && b.Coupon.Code.Contains(search)) ||
                    (b.PromoCode != null && b.PromoCode.Code.Contains(search)));
            }


            if (page < 1)
                page = 1;

            int currentPage = page;
            double totalPages = Math.Ceiling(bookings.Count() / 5.0);

            var pagedBookings = bookings
                .Skip((currentPage - 1) * 5)
                .Take(5)
                .ToList();

            return View(new BookingVM
            {
                Bookings = pagedBookings,
                CurrentPage = currentPage,
                TotalPages = totalPages
            });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var booking = await _bookingRepo.GetOneAsync(
                b => b.Id == id,
                includes: new Expression<Func<Booking, object>>[]
                {
            b => b.Address,
            b => b.Client,
            b => b.Worker,
            b => b.Coupon,
            b => b.PromoCode,
            b => b.BookingItems
                },
                tracking: false);

            if (booking is null)
                return NotFound();

            return View(new BookingDetailsVM
            {
                Booking = booking,
                ItemCount = booking.BookingItems?.Count ?? 0,
                ItemsTotal = booking.BookingItems?.Sum(i => i.Quantity * i.UnitPrice) ?? 0
            });
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync(booking);
                return View(booking);
            }

            await _bookingRepo.CreateAsync(booking);
            await _bookingRepo.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var booking = await _bookingRepo.GetOneAsync(b => b.Id == id, tracking: false);

            if (booking is null)
                return NotFound();

            await LoadDropdownsAsync(booking);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownsAsync(booking);
                return View(booking);
            }

            var existingBooking = await _bookingRepo.GetOneAsync(b => b.Id == booking.Id);

            if (existingBooking is null)
                return NotFound();

            existingBooking.ClientId = booking.ClientId;
            existingBooking.WorkerId = booking.WorkerId;
            existingBooking.AddressId = booking.AddressId;
            existingBooking.CouponId = booking.CouponId;
            existingBooking.PromoCodeId = booking.PromoCodeId;
            existingBooking.Status = booking.Status;

            _bookingRepo.Update(existingBooking);
            await _bookingRepo.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await _bookingRepo.GetOneAsync(b => b.Id == id);

            if (booking is null)
                return NotFound();

            _bookingRepo.Delete(booking);
            await _bookingRepo.CommitAsync();
            return Ok();
        }

        private async Task LoadDropdownsAsync(Booking? booking = null)
        {
            var clients = await _userRepo.GetAsync(tracking: false);
            var workers = await _workerprofileRepo.GetAsync(tracking: false);
            var addresses = await _addressRepo.GetAsync(tracking: false);
            var coupons = await _couponRepo.GetAsync(tracking: false);
            var promoCodes = await _promoCodeRepo.GetAsync(tracking: false);

            ViewData["ClientId"] = new SelectList(clients, "Id", "UserName", booking?.ClientId);
            ViewData["WorkerId"] = new SelectList(workers, "Id", "Id", booking?.WorkerId);
            ViewData["AddressId"] = new SelectList(addresses, "Id", "Id", booking?.AddressId);
            ViewData["CouponId"] = new SelectList(coupons, "Id", "Code", booking?.CouponId);
            ViewData["PromoCodeId"] = new SelectList(promoCodes, "Id", "Code", booking?.PromoCodeId);
            ViewData["StatusList"] = new SelectList(Enum.GetValues(typeof(BookingStatus)), booking?.Status);
        }
    }
}
