using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public class UnAvalability
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [MaxLength(255)]
        public string Reason { get; set; }
        [Required]
        public int WorkerId { get; set; }
        [ForeignKey(nameof(WorkerId))]
        public WorkerProfile Worker { get; set; }
    }
}
