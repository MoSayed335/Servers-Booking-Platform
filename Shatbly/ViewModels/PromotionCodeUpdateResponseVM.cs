namespace Shatbly.ViewModels
{
    public class PromotionCodeUpdateResponseVM
    {
        public PromotionCode PromotionCode { get; set; } = new();
        public IEnumerable<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
