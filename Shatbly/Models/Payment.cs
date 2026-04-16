using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public enum PaymentMethod
    {
        CreditCard,
        DebitCard,
        Wallet,
        Cash,
        BankTransfer
    }

    public enum PaymentStatus
    {
        Pending,
        Paid,
        Failed,
        Refunded
    }
    public class Payment
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }

        [Required]
        public PaymentMethod Method { get; set; }

        [Required]
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        [MaxLength(100)]
        public string GatewayName { get; set; }

        [MaxLength(255)]
        public string GatewayRef { get; set; }

        public string GatewayResponse { get; set; }

        [MaxLength(255)]
        public string TransactionId { get; set; }

        public DateTime? PaidAt { get; set; }

        [Required]
        public int BookingId { get; set; }
        [ForeignKey(nameof(BookingId))]
        public  Booking Booking { get; set; }
    }
}
