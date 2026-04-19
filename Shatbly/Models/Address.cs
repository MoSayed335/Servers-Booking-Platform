using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string City { get; set; }
        [MaxLength(100)]
        public string District { get; set; }
        [MaxLength(255)]
        public string Street { get; set; }
        [Column(TypeName = "float")]
        public double? Lat { get; set; }
        [Column(TypeName = "float")]
        public double? Lng { get; set; }
        public bool IsDefault { get; set; } = false;
        [Required]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public  User User { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
