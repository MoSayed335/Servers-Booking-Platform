using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public class WorkerProfile
    {
        [Key]
        public int Id { get; set; }

        public string Bio { get; set; }

        [Column(TypeName = "decimal(3,2)")]
        [Range(0.0, 5.0)]
        public decimal RatingAvg { get; set; } = 0;

        public int RatingCount { get; set; } = 0;

        public bool IsVerified { get; set; } = false;

        public bool IsAvailable { get; set; } = true;

        public bool AcceptsOnline { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

        public WorkerService WorkerServices { get; set; } 
        public ICollection<Avalability> Availabilities { get; set; }
        public ICollection<UnAvalability> Unavailabilities { get; set; }
        public ICollection<PortfolioMedia> PortfolioMediaItems { get; set; }
        public ICollection<Booking> WorkerBookings { get; set; } 
        public ICollection<Review> WorkerReviews { get; set; }
    }
}
