namespace Shatbly.ViewModels
{
    public class CouponsVM
    {
        public IEnumerable<Coupon> Coupons { get; set; } = null!;
        public int CurrentPage { get; set; }
        public double TotalPages { get; set; }
    }
}
