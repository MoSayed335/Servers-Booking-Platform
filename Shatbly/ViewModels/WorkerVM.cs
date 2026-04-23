namespace Shatbly.ViewModels
{
    public class WorkerVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Last Name")]
        public string LName { get; set; } = string.Empty;
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;
        [Phone]
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public IFormFile cv { get; set; }

    }
}
