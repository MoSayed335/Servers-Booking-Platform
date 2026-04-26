using Microsoft.AspNetCore.Mvc;

namespace Shatbly.Areas.Admin.Controllers
{
    public class PromotionCodeController : Controller
    {
        private readonly IRepository<PromotionCode> _promotionCodeRepository;
        private readonly IRepository<Promotion> _promotionRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public PromotionCodeController(IRepository<PromotionCode> promotionCodeRepository, IRepository<Promotion> promotionRepository, IRepository<Booking> bookingRepository)
        {
            _promotionCodeRepository = promotionCodeRepository;
            _promotionRepository = promotionRepository;
            _bookingRepository = bookingRepository;
        }
        public async Task<IActionResult> Index(string? code, int page = 1)
        {
            var promotionCodes = await _promotionCodeRepository.GetAsync(includes: [p => p.Promotion], tracking: false);
            if (code is not null)
            {
                promotionCodes = promotionCodes.Where(p => p.Code.Contains(code)).ToList();
            }
            if (page < 1) page = 1;

            int currentPage = page;
            double totalPages = Math.Ceiling(promotionCodes.Count() / 5.0);
            promotionCodes = promotionCodes.Skip((page - 1) * 5).Take(5).ToList();

            return View(new PromotionCodesVM
            {
                PromotionCodes = promotionCodes.AsEnumerable(),
                CurrentPage = currentPage,
                TotalPages = totalPages
            });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var bookings = await _bookingRepository.GetAsync(tracking: false);

            return View(new PromotionCodeCreateVM
            {
                PromotionCode = new PromotionCode(),
                Bookings = bookings.AsEnumerable(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(PromotionCodeCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Bookings = await _bookingRepository.GetAsync(tracking: false);
                return View(vm);
            }
            await _promotionCodeRepository.CreateAsync(vm.PromotionCode);
            await _promotionCodeRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var promotionCode = await _promotionCodeRepository.GetOneAsync(p => p.Id == id);

            if (promotionCode is null) return NotFound();

            var bookings = await _bookingRepository.GetAsync(tracking: false);

            return View(new PromotionCodeUpdateResponseVM
            {
                PromotionCode = promotionCode,
                Bookings = bookings.AsEnumerable(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PromotionCodeUpdateResponseVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Bookings = await _bookingRepository.GetAsync(tracking: false);
                return View(vm);
            }

            _promotionCodeRepository.Update(vm.PromotionCode);
            await _promotionCodeRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var promotionCode = await _promotionCodeRepository.GetOneAsync(p => p.Id == id);
            if (promotionCode is null) return NotFound();

            _promotionCodeRepository.Delete(promotionCode);
            await _promotionCodeRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
