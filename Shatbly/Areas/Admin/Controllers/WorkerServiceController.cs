using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace Shatbly.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    //[Authorize(Roles = $"{SD.ROLE_ADMIN},{SD.ROLE_SUPER_ADMIN}")]
    public class WorkerServiceController : Controller
    {
        private readonly IRepository<WorkerService> _workerServiceRepo;
        private readonly IRepository<ServiceCategory> _categoryRepo;
        private readonly IRepository<WorkerProfile> _workerRepo;

        public WorkerServiceController(
            IRepository<WorkerService> workerServiceRepo,
            IRepository<ServiceCategory> categoryRepo,
            IRepository<WorkerProfile> workerRepo)
        {
            _workerServiceRepo = workerServiceRepo;
            _categoryRepo = categoryRepo;
            _workerRepo = workerRepo;
        }

        // ───────── INDEX ─────────
        public async Task<IActionResult> Index(string? search, int page = 1)
        {
            var includes = new Expression<Func<WorkerService, object>>[]
            {
                ws => ws.Category,
                ws => ws.Worker,
                ws => ws.Worker.User
            };

            var workerServices = await _workerServiceRepo.GetAsync(includes: includes, tracking: false);

            // Filter by category name or worker name
            if (!string.IsNullOrWhiteSpace(search))
            {
                workerServices = workerServices
                    .Where(ws =>
                        (ws.Category?.NameEn != null && ws.Category.NameEn.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (ws.Category?.NameAr != null && ws.Category.NameAr.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (ws.Worker?.User?.FName != null && ws.Worker.User.FName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (ws.Worker?.User?.LName != null && ws.Worker.User.LName.Contains(search, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }

            if (page < 1) page = 1;
            int pageSize = 5;
            int currentPage = page;
            double totalPages = Math.Ceiling(workerServices.Count() / (double)pageSize);
            workerServices = workerServices.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(new WorkerServicesVM
            {
                WorkerServices = workerServices,
                CurrentPage = currentPage,
                TotalPages = totalPages
            });
        }

        // ───────── CREATE (GET) ─────────
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new WorkerServicesVM
            {
                Categories = await _categoryRepo.GetAsync(e => e.IsActive, tracking: false),
                Workers = await _workerRepo.GetAsync(
                    includes: new Expression<Func<WorkerProfile, object>>[] { w => w.User },
                    tracking: false)
            };
            return View(vm);
        }

        // ───────── CREATE (POST) ─────────
        [HttpPost]
        public async Task<IActionResult> Create(WorkerServicesVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _categoryRepo.GetAsync(e => e.IsActive, tracking: false);
                model.Workers = await _workerRepo.GetAsync(
                    includes: new Expression<Func<WorkerProfile, object>>[] { w => w.User },
                    tracking: false);
                TempData["error-notification"] = "Please correct the errors in the form.";
                return View(model);
            }

            var workerService = new WorkerService
            {
                WorkerId = model.WorkerId,
                CategoryId = model.CategoryId,
                HourlyRate = model.HourlyRate,
                IsActive = model.IsActive
            };

            await _workerServiceRepo.CreateAsync(workerService);
            await _workerServiceRepo.CommitAsync();
            TempData["success-notification"] = "Worker service created successfully";
            return RedirectToAction(nameof(Index));
        }

        // ───────── EDIT (GET) ─────────
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var includes = new Expression<Func<WorkerService, object>>[]
            {
                ws => ws.Category,
                ws => ws.Worker
            };

            var ws = await _workerServiceRepo.GetOneAsync(e => e.Id == id, includes: includes, tracking: false);
            if (ws is null)
            {
                TempData["error-notification"] = "Worker service not found.";
                return NotFound();
            }

            var vm = new WorkerServicesVM
            {
                Id = ws.Id,
                WorkerId = ws.WorkerId,
                CategoryId = ws.CategoryId,
                HourlyRate = ws.HourlyRate,
                IsActive = ws.IsActive,
                Categories = await _categoryRepo.GetAsync(e => e.IsActive, tracking: false),
                Workers = await _workerRepo.GetAsync(
                    includes: new Expression<Func<WorkerProfile, object>>[] { w => w.User },
                    tracking: false)
            };

            return View(vm);
        }

        // ───────── EDIT (POST) ─────────
        [HttpPost]
        public async Task<IActionResult> Edit(WorkerServicesVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _categoryRepo.GetAsync(e => e.IsActive, tracking: false);
                model.Workers = await _workerRepo.GetAsync(
                    includes: new Expression<Func<WorkerProfile, object>>[] { w => w.User },
                    tracking: false);
                TempData["error-notification"] = "Please correct the errors in the form.";
                return View(model);
            }

            var existing = await _workerServiceRepo.GetOneAsync(e => e.Id == model.Id, tracking: false);
            if (existing is null)
            {
                TempData["error-notification"] = "Worker service not found.";
                return NotFound();
            }

            var workerService = new WorkerService
            {
                Id = model.Id,
                WorkerId = model.WorkerId,
                CategoryId = model.CategoryId,
                HourlyRate = model.HourlyRate,
                IsActive = model.IsActive
            };

            _workerServiceRepo.Update(workerService);
            await _workerServiceRepo.CommitAsync();
            TempData["success-notification"] = "Worker service updated successfully";
            return RedirectToAction(nameof(Index));
        }

        // ───────── DELETE ─────────
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var ws = await _workerServiceRepo.GetOneAsync(e => e.Id == id);
            if (ws is null)
            {
                TempData["error-notification"] = "Worker service not found.";
                return NotFound();
            }

            _workerServiceRepo.Delete(ws);
            await _workerServiceRepo.CommitAsync();
            TempData["success-notification"] = "Worker service deleted successfully";
            return Ok();
        }
    }
}
