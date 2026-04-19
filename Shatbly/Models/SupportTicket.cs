using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public enum SupportTicketStatus
    {
        Open,
        InProgress,
        Resolved,
        Closed
    }

    public enum SupportTicketPriority
    {
        Low,
        Medium,
        High,
        Urgent
    }
    public class SupportTicket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Subject { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public SupportTicketStatus Status { get; set; } = SupportTicketStatus.Open;

        [Required]
        public SupportTicketPriority Priority { get; set; } = SupportTicketPriority.Medium;

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
