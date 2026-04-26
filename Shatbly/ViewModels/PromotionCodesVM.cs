namespace Shatbly.ViewModels
{
    public class PromotionCodesVM
    {
        public IEnumerable<PromotionCode> PromotionCodes { get; set; }
        public int CurrentPage { get; set; }
        public double TotalPages { get; set; }
    }
}
