namespace Shatbly.ViewModels
{
    public class LoginVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Email or Username")]
        public string EmailOrUserName { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
