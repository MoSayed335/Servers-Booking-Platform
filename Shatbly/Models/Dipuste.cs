using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public enum DisputeStatus
    {
        Open,
        UnderReview,
        Resolved,
        Closed
    }

    public class Dipuste
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(255)]
        public string Reason { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DisputeStatus Status { get; set; } = DisputeStatus.Open;

        public string Resolution { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public int BookingId { get; set; }
        [ForeignKey(nameof(BookingId))]
        public  Booking Booking { get; set; }

        [Required]
        public int RaisedById { get; set; }
        [ForeignKey(nameof(RaisedById))]
        public  User RaisedBy { get; set; }
        [Required]
        public int AgainstId { get; set; }
        [ForeignKey(nameof(AgainstId))]
        public  User Against { get; set; }
    }
}
