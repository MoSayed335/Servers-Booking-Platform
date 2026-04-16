using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public class Wallet
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Balance { get; set; } = 0;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public ICollection<WalletTransaction> Transactions { get; set; }
    }
}
