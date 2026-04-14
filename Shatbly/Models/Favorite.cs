using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shatbly.Models
{
    public class Favorite
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int WorkerId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public int ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public  User Client { get; set; }

        [ForeignKey(nameof(WorkerId))]
        public  WorkerProfile Worker { get; set; }
    }
}
