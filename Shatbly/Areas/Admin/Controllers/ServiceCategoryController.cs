using Microsoft.AspNetCore.Mvc;

namespace Shatbly.Areas.Admin.Controllers
{
    public class ServiceCategoryController : Controller
    {
        private IRepository<ServiceCategory> _serviceCategoryRepository;
        public ServiceCategoryController(IRepository<ServiceCategory> categoryRepository)
        {
            _serviceCategoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index(string? name, int page = 1)
        {
            var serviceCategories = await _serviceCategoryRepository.GetAsync(tracking: false);
            if (name is not null)
            {
                serviceCategories = serviceCategories.Where(c => c.NameAr.Contains(name) || c.NameEn.Contains(name)).ToList();
            }

            if (page < 1) page = 1;

            int currentPage = page;
            double totalPages = Math.Ceiling(serviceCategories.Count() / 5.0);
            serviceCategories = serviceCategories.Skip((page - 1) * 5).Take(5).ToList();

            return View(new ServiceCategoriesVM
            {
                ServiceCategories = serviceCategories.AsEnumerable(),
                CurrentPage = currentPage,
                TotalPages = totalPages
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceCategory serviceCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(serviceCategory);
            }

            await _serviceCategoryRepository.CreateAsync(serviceCategory);
            await _serviceCategoryRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var serviceCategory = await _serviceCategoryRepository.GetOneAsync(c => c.Id == id);
            if (serviceCategory is null)
            {
                return NotFound();
            }
            return View(serviceCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ServiceCategory serviceCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(serviceCategory);
            }

            _serviceCategoryRepository.Update(serviceCategory);
            await _serviceCategoryRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var serviceCategory = await _serviceCategoryRepository.GetOneAsync(c => c.Id == id);
            if (serviceCategory is null)
            {
                return NotFound();
            }

            _serviceCategoryRepository.Delete(serviceCategory);
            await _serviceCategoryRepository.CommitAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
