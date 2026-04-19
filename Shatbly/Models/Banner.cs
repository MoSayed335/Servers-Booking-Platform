using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public enum BannerTarget
    {
        Home,
        Category,
        Worker,
        Promotion
    }
    public class Banner
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1024)]
        [Url]
        public string ImageUrl { get; set; }

        [MaxLength(1024)]
        [Url]
        public string LinkUrl { get; set; }

        [Required]
        public BannerTarget Target { get; set; }
        public int? TargetId { get; set; }

        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}
