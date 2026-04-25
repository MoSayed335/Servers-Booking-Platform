using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Shatbly.Models;

namespace Shatbly.ViewModels
{
    public class WorkerProfilesVM
    {
        // Lists for the Index view
        public IEnumerable<WorkerProfile>? WorkerProfiles { get; set; }
        public IEnumerable<User>? Users { get; set; }
        public int CurrentPage { get; set; }
        public double TotalPages { get; set; }

        // Fields used for Create / Edit forms
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a user.")]
        [Display(Name = "User")]
        public string UserId { get; set; } = string.Empty;

        public string? Bio { get; set; }

        [Display(Name = "Verified")]
        public bool IsVerified { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; } = true;

        [Display(Name = "Accepts Online")]
        public bool AcceptsOnline { get; set; }

        [Display(Name = "Approved")]
        public bool IsApproved { get; set; }

        [Display(Name = "Interview Date")]
        public DateTime? InterviewDate { get; set; }

        [Display(Name = "HR Notes")]
        public string? HRNotes { get; set; }

        [Display(Name = "CV File")]
        public IFormFile? CVFile { get; set; }
        
        public string? ExistingCVPath { get; set; }
    }
}
