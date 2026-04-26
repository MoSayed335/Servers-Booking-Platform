namespace Shatbly.ViewModels
{
    public class BookingVM
    {
        public IEnumerable<Booking> Bookings { get; set; }
        public int CurrentPage { get; set; }
        public double TotalPages { get; set; }

    }
}
