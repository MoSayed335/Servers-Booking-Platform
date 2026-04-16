using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
     public enum ReferralStatus
    {
        Pending,
        Completed,
        Expired
    }
    public class Referral
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal RewardAmount { get; set; } = 0;

        [Required]
        public ReferralStatus Status { get; set; } = ReferralStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public int ReferrerId { get; set; }
        [ForeignKey(nameof(ReferrerId))]
        public virtual User Referrer { get; set; }

        [Required]
        public int ReferredId { get; set; }
        [ForeignKey(nameof(ReferredId))]
        public User Referred { get; set; }
    }
}
