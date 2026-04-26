using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Shatbly.Models;
using Shatbly.ViewModels;
using Shatbly.Utilities;
using System.IO;

namespace Shatbly.Areas.Admin.Controllers
{
    [Area(SD.ADMIN_AREA)]
    //[Authorize(Roles = $"{SD.ROLE_ADMIN},{SD.ROLE_SUPER_ADMIN}")]
    public class WorkerProfileController : Controller
    {
        private readonly IRepository<WorkerProfile> _workerProfileRepo;
        private readonly IRepository<User> _userRepo;

        public WorkerProfileController(
            IRepository<WorkerProfile> workerProfileRepo,
            IRepository<User> userRepo)
        {
            _workerProfileRepo = workerProfileRepo;
            _userRepo = userRepo;
        }

        // ───────── INDEX ─────────
        public async Task<IActionResult> Index(string? search, int page = 1)
        {
            var includes = new Expression<Func<WorkerProfile, object>>[]
            {
                wp => wp.User
            };

            var workerProfiles = await _workerProfileRepo.GetAsync(includes: includes, tracking: false);

            // Filter by user name or email
            if (!string.IsNullOrWhiteSpace(search))
            {
                workerProfiles = workerProfiles
                    .Where(wp =>
                        (wp.User?.FName != null && wp.User.FName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (wp.User?.LName != null && wp.User.LName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                        (wp.User?.Email != null && wp.User.Email.Contains(search, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }

            if (page < 1) page = 1;
            int pageSize = 5;
            int currentPage = page;
            double totalPages = Math.Ceiling(workerProfiles.Count() / (double)pageSize);
            workerProfiles = workerProfiles.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return View(new WorkerProfilesVM
            {
                WorkerProfiles = workerProfiles,
                CurrentPage = currentPage,
                TotalPages = totalPages
            });
        }

        // ───────── CREATE (GET) ─────────
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new WorkerProfilesVM
            {
                Users = await _userRepo.GetAsync(tracking: false)
            };
            return View(vm);
        }

        // ───────── CREATE (POST) ─────────
        [HttpPost]
        public async Task<IActionResult> Create(WorkerProfilesVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Users = await _userRepo.GetAsync(tracking: false);
                TempData["error-notification"] = "Please correct the errors in the form.";
                return View(model);
            }

            var workerProfile = new WorkerProfile
            {
                UserId = model.UserId,
                Bio = model.Bio,
                IsVerified = model.IsVerified,
                IsAvailable = model.IsAvailable,
                AcceptsOnline = model.AcceptsOnline,
                IsApproved = model.IsApproved,
                InterviewDate = model.InterviewDate,
                HRNotes = model.HRNotes
            };

            if (model.CVFile is not null)
            {
                var newFileName = Guid.NewGuid().ToString() + DateTime.UtcNow.ToString("yyyy-MM-dd") + Path.GetExtension(model.CVFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\worker\\worker_cv", newFileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await model.CVFile.CopyToAsync(stream);
                }
                workerProfile.CVPath = newFileName;
            }

            await _workerProfileRepo.CreateAsync(workerProfile);
            await _workerProfileRepo.CommitAsync();
            TempData["success-notification"] = "Worker profile created successfully";
            return RedirectToAction(nameof(Index));
        }

        // ───────── EDIT (GET) ─────────
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var includes = new Expression<Func<WorkerProfile, object>>[]
            {
                wp => wp.User
            };

            var wp = await _workerProfileRepo.GetOneAsync(e => e.Id == id, includes: includes, tracking: false);
            if (wp is null)
            {
                TempData["error-notification"] = "Worker profile not found.";
                return NotFound();
            }

            var vm = new WorkerProfilesVM
            {
                Id = wp.Id,
                UserId = wp.UserId,
                Bio = wp.Bio,
                IsVerified = wp.IsVerified,
                IsAvailable = wp.IsAvailable,
                AcceptsOnline = wp.AcceptsOnline,
                IsApproved = wp.IsApproved,
                InterviewDate = wp.InterviewDate,
                HRNotes = wp.HRNotes,
                ExistingCVPath = wp.CVPath,
                Users = await _userRepo.GetAsync(tracking: false)
            };

            return View(vm);
        }

        // ───────── EDIT (POST) ─────────
        [HttpPost]
        public async Task<IActionResult> Edit(WorkerProfilesVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Users = await _userRepo.GetAsync(tracking: false);
                TempData["error-notification"] = "Please correct the errors in the form.";
                return View(model);
            }

            var existing = await _workerProfileRepo.GetOneAsync(e => e.Id == model.Id, tracking: false);
            if (existing is null)
            {
                TempData["error-notification"] = "Worker profile not found.";
                return NotFound();
            }

            var workerProfile = new WorkerProfile
            {
                Id = model.Id,
                UserId = model.UserId,
                Bio = model.Bio,
                IsVerified = model.IsVerified,
                IsAvailable = model.IsAvailable,
                AcceptsOnline = model.AcceptsOnline,
                IsApproved = model.IsApproved,
                InterviewDate = model.InterviewDate,
                HRNotes = model.HRNotes,
                CVPath = existing.CVPath,
                RatingAvg = existing.RatingAvg,
                RatingCount = existing.RatingCount,
                CreatedAt = existing.CreatedAt
            };

            if (model.CVFile is not null)
            {
                var newFileName = Guid.NewGuid().ToString() + DateTime.UtcNow.ToString("yyyy-MM-dd") + Path.GetExtension(model.CVFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\worker\\worker_cv", newFileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await model.CVFile.CopyToAsync(stream);
                }
                
                if (!string.IsNullOrEmpty(existing.CVPath))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\worker\\worker_cv", existing.CVPath);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                
                workerProfile.CVPath = newFileName;
            }

            _workerProfileRepo.Update(workerProfile);
            await _workerProfileRepo.CommitAsync();
            TempData["success-notification"] = "Worker profile updated successfully";
            return RedirectToAction(nameof(Index));
        }

        // ───────── DELETE ─────────
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var wp = await _workerProfileRepo.GetOneAsync(e => e.Id == id);
            if (wp is null)
            {
                TempData["error-notification"] = "Worker profile not found.";
                return NotFound();
            }

            if (!string.IsNullOrEmpty(wp.CVPath))
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\worker\\worker_cv", wp.CVPath);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            _workerProfileRepo.Delete(wp);
            await _workerProfileRepo.CommitAsync();
            TempData["success-notification"] = "Worker profile deleted successfully";
            return Ok();
        }
    }
}
