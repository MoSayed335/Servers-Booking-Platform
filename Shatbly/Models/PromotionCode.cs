using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public class PromotionCode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        public int MaxUses { get; set; }

        public int UsedCount { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        [Required]
        public int PromotionId { get; set; }
        [ForeignKey(nameof(PromotionId))]
        public  Promotion Promotion { get; set; }

        public  ICollection<Booking> Bookings { get; set; }
    }
}
