using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public class BookingItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }
        [Required]
        public int BookingId { get; set; }

        [ForeignKey(nameof(BookingId))]
        public virtual Booking Booking { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public  ServiceCategory Category { get; set; }
    }
}
