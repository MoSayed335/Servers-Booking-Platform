namespace Shatbly.ViewModels
{
    public class PromotionsVM
    {
        public IEnumerable<Promotion> Promotions { get; set; }
        public int CurrentPage { get; set; }
        public double TotalPages { get; set; }
    }
}
