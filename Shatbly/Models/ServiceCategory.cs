using System.ComponentModel.DataAnnotations;

namespace Shatbly.Models
{
    public class ServiceCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string NameAr { get; set; }

        [Required]
        [MaxLength(150)]
        public string NameEn { get; set; }

        [MaxLength(255)]
        public string Icon { get; set; }

        public bool IsActive { get; set; } = true;
        public ICollection<WorkerService> WorkerServices { get; set; }
        public ICollection<Coupon> Coupons { get; set; }
        public ICollection<BookingItem> BookingItems { get; set; }
        public ICollection<Promotion> Promotions { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
