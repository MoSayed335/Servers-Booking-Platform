using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        InProgress,
        Completed,
        Cancelled,
        Disputed
    }
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime ScheduledAt { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1 hour.")]
        public int DurationHours { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal DiscountAmt { get; set; } = 0;

        [Required]
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public string ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public User Client { get; set; }
        [Required]
        public int WorkerId { get; set; }
        [ForeignKey(nameof(WorkerId))]
        public WorkerProfile Worker { get; set; }
        [Required]
        public int AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; }
        public int? CouponId { get; set; }
        [ForeignKey(nameof(CouponId))]
        public Coupon Coupon { get; set; }
        public int? PromoCodeId { get; set; }
        [ForeignKey(nameof(PromoCodeId))]
        public PromotionCode? PromoCode { get; set; }
        public Payment Payment { get; set; }
        public Review Review { get; set; }
        public ICollection<BookingItem> BookingItems { get; set; } 
        public ICollection<ChatMessage> ChatMessages { get; set; }
        public ICollection<SupportTicket> SupportTickets { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<LogActivity> LogActivities { get; set; } 
        public ICollection<Dipuste> Disputes { get; set; }
    }
}
