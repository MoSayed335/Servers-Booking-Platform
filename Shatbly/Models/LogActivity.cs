using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public class LogActivity
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Action { get; set; }

        public string Details { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public int? BookingId { get; set; }
        [ForeignKey(nameof(BookingId))]
        public Booking Booking { get; set; }
    }
}
