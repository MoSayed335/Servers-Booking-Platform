namespace Shatbly.ViewModels
{
    public class WorkerServicesVM
    {
        public IEnumerable<WorkerService>? WorkerServices { get; set; }
        public IEnumerable<ServiceCategory>? Categories { get; set; }
        public IEnumerable<WorkerProfile>? Workers { get; set; }
        public int CurrentPage { get; set; }
        public double TotalPages { get; set; }

        // Fields used for Create / Edit forms
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a worker.")]
        [Display(Name = "Worker")]
        public int WorkerId { get; set; }

        [Required(ErrorMessage = "Please select a service category.")]
        [Display(Name = "Service Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Hourly rate is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Hourly rate must be greater than 0.")]
        [Display(Name = "Hourly Rate")]
        public decimal HourlyRate { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }
}
