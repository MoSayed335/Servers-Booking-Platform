namespace Shatbly.ViewModels
{
    public class CreateUserVM
    {
        public string Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public string RoleName { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
