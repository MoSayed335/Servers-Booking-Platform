namespace Shatbly.ViewModels
{
    public class PromotionUpdateResponseVM
    {
        public Promotion Promotion { get; set; } = new();
        public IEnumerable<ServiceCategory> ServiceCategories { get; set; } = new List<ServiceCategory>();
    }
}
