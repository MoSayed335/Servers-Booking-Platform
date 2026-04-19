using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Shatbly.Models
{
    public enum Platform
    {
        Android,
        iOS,
        Web
    }
    public class DeviceToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(512)]
        public string Token { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public Platform Platform { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
