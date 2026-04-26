namespace Shatbly.ViewModels
{
    public class ServiceCategoriesVM
    {
        public IEnumerable<ServiceCategory> ServiceCategories { get; set; }
        public int CurrentPage { get; set; }
        public double TotalPages { get; set; }
    }
}
