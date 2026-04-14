using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public class WorkerService
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Hourly rate must be greater than 0.")]
        public decimal HourlyRate { get; set; }

        public bool IsActive { get; set; } = true;
        [Required]
        public int WorkerId { get; set; }

        [ForeignKey(nameof(WorkerId))]
        public WorkerProfile Worker { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public ServiceCategory Category { get; set; }
        public ICollection<BookingItem> BookingItems { get; set; }
    }
}
