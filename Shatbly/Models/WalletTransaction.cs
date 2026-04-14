using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public enum WalletTransactionType
    {
        Deposit,
        Withdrawal,
        Refund,
        Earning,
        Deduction
    }
    public class WalletTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Amount { get; set; }

        [Required]
        public WalletTransactionType Type { get; set; }

        [MaxLength(512)]
        public string Reference { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public int WalletId { get; set; }
        [ForeignKey(nameof(WalletId))]
        public  Wallet Wallet { get; set; }
    }
}
