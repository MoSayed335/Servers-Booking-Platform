using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public class RefereshToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(512)]
        public string Token { get; set; }
        [MaxLength(255)]
        public string DeviceInfo { get; set; }
        [Required]
        public DateTime ExpiresAt { get; set; }
        public DateTime? RevokedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
