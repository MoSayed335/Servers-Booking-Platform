using Microsoft.Build.Tasks;

namespace Shatbly.ViewModels
{
    public class PromotionCreateVM
    {
        public Promotion Promotion { get; set; } = new();
        public IEnumerable<ServiceCategory> ServiceCategories { get; set; } = new List<ServiceCategory>();
    }
}
