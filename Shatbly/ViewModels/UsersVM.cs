namespace Shatbly.ViewModels
{
    public class UsersVM
    {
        public IEnumerable<UserWithRoleVM> Users { get; set; }
        public double TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
