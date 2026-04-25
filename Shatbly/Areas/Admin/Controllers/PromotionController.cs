using Azure;
using Microsoft.AspNetCore.Mvc;

namespace Shatbly.Areas.Admin.Controllers
{
    public class PromotionController : Controller
    {
        private readonly IRepository<Promotion> _promotionRepository;
        private readonly IRepository<ServiceCategory> _serviceCategoryRepository;

        public PromotionController(IRepository<Promotion> promotionRepository, IRepository<ServiceCategory> serviceCategoryRepository)
        {
            _promotionRepository = promotionRepository;
            _serviceCategoryRepository = serviceCategoryRepository;
        }

        public async Task<IActionResult> Index(string? title, int page = 1)
        {
            var promotions = await _promotionRepository.GetAsync(includes: [p => p.Category], tracking: false);
            if (title is not null)
            {
                promotions = promotions.Where(p => p.Title.Contains(title)).ToList();
            }
            if (page < 1) page = 1;

            int currentPage = page;
            double totalPages = Math.Ceiling(promotions.Count() / 5.0);
            promotions = promotions.Skip((page - 1) * 5).Take(5).ToList();

            return View(new PromotionsVM
            {
                Promotions = promotions.AsEnumerable(),
                CurrentPage = currentPage,
                TotalPages = totalPages
            });
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var serviceCategories = await _serviceCategoryRepository.GetAsync(tracking: false);

            return View(new PromotionCreateVM
            {
                Promotion = new Promotion(),
                ServiceCategories = serviceCategories.AsEnumerable(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(PromotionCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.ServiceCategories = await _serviceCategoryRepository.GetAsync(tracking: false);
                return View(vm);
            }
            await _promotionRepository.CreateAsync(vm.Promotion);
            await _promotionRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            var promotion = await _promotionRepository.GetOneAsync(p => p.Id == id);

            if (promotion is null) return NotFound();

            var serviceCategories = await _serviceCategoryRepository.GetAsync(tracking: false);

            return View(new PromotionUpdateResponseVM
            {
                Promotion = promotion,
                ServiceCategories = serviceCategories.AsEnumerable(),
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PromotionUpdateResponseVM vm)
        {
            if (!ModelState.IsValid)
            {
                vm.ServiceCategories = await _serviceCategoryRepository.GetAsync(tracking: false);
                return View(vm);
            }

            _promotionRepository.Update(vm.Promotion);
            await _promotionRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var promotion = await _promotionRepository.GetOneAsync(p => p.Id == id);
            if (promotion is null) return NotFound();

            _promotionRepository.Delete(promotion);
            await _promotionRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
