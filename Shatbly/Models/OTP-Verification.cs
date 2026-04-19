using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{

    public enum OtpType
    {
        PhoneVerification,
        EmailVerification,
        PasswordReset
    }
    public class OTP_Verification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Code { get; set; }
        [Required]
        public OtpType Type { get; set; }
        [Required]
        [MaxLength(255)]
        public string PhoneEmail { get; set; }
        [Required]
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
