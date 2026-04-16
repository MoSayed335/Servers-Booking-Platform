using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public enum DayOfWeek
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday
    }
    public class Avalability
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DayOfWeek DayOfWeek { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        [Required]
        public int WorkerId { get; set; }
        [ForeignKey(nameof(WorkerId))]
        public WorkerProfile Worker { get; set; }
    }
}
