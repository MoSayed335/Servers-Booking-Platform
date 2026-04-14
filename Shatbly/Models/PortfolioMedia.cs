using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Shatbly.Models
{

    public enum MediaType
    {
        Image,
        Video
    }
    public class PortfolioMedia
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(1024)]
        [Url]
        public string MediaUrl { get; set; }

        [Required]
        public MediaType MediaType { get; set; }

        [MaxLength(500)]
        public string Caption { get; set; }

        public bool IsPrimary { get; set; } = false;

        [Required]
        public int WorkerId { get; set; }

        [ForeignKey(nameof(WorkerId))]
        public WorkerProfile Worker { get; set; }
    }
}
