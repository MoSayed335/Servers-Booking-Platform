namespace Shatbly.ViewModels
{
    public class EditUserVM
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Last Name")]
        public string LName { get; set; } = string.Empty;
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string RoleName { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
