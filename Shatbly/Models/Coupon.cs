using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public class Coupon
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, double.MaxValue)]
        public decimal DiscountValue { get; set; }

        [Required]
        public DiscountType DiscountType { get; set; }

        public int MaxUses { get; set; }

        public int UsedCount { get; set; } = 0;

        [Required]
        public DateTime ValidFrom { get; set; }

        [Required]
        public DateTime ValidUntil { get; set; }

        public bool IsActive { get; set; } = true;

        public int? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public  ServiceCategory Category { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
