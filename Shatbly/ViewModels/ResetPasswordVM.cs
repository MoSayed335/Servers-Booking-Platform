namespace Shatbly.ViewModels
{
    public class ResetPasswordVM
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        [Required]
        public string NewPassword { get; set; } = string.Empty;
        [Required]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
