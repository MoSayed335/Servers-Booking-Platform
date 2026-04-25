namespace Shatbly.ViewModels
{
    public class BannersVM
    {
        public IEnumerable<Banner> Banners { get; set; }
        public int CurrentPage { get; set; }
        public double TotalPages { get; set; }
    }
}
