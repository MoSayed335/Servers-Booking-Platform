using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public enum NotificationType
    {
        BookingUpdate,
        PaymentUpdate,
        Promotion,
        Message,
        System
    }
    public class Notification
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public NotificationType Type { get; set; }

        public bool IsRead { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public  User User { get; set; }
        public int? BookingId { get; set; }
        [ForeignKey(nameof(BookingId))]
        public  Booking Booking { get; set; }
    }
}
