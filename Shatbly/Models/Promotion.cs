using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public enum DiscountType
    {
        Percentage,
        FixedAmount
    }

    public class Promotion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, double.MaxValue)]
        public decimal DiscountValue { get; set; }

        [Required]
        public DiscountType DiscountType { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal MinOrderValue { get; set; } = 0;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; } = true;

        public int? CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public ServiceCategory Category { get; set; }

        public ICollection<PromotionCode> PromotionCodes { get; set; } 
    }
}
