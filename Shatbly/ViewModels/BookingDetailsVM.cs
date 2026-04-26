namespace Shatbly.ViewModels
{
    public class BookingDetailsVM
    {
        public Booking Booking { get; set; } = null!;
        public decimal ItemsTotal { get; set; }
        public int ItemCount { get; set; }
    }
}
