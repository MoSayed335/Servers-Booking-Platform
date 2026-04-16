using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Shatbly.Models
{
    public enum UserRole
    {
        Client,
        Worker,
        Admin
    }
    public class User : IdentityUser<int>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(20)]
        [Phone]
        public string Phone { get; set; }
        [Required]
        [MaxLength(512)]
        public string PasswordHash { get; set; }
        [Required]
        public UserRole Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public WorkerProfile WorkerProfile { get; set; }  
        public Wallet Wallet { get; set; }                

        public ICollection<Address> Addresses { get; set; }
        public ICollection<OTP_Verification> OtpVerifications { get; set; }
        public ICollection<RefereshToken> RefreshTokens { get; set; }
        public ICollection<DeviceToken> DeviceTokens { get; set; }
        public ICollection<Booking> ClientBookings { get; set; }
        public ICollection<Favorite> Favorites { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<SupportTicket> SupportTickets { get; set; }
        public ICollection<LogActivity> LogActivities { get; set; }
        public ICollection<ChatMessage> SentMessages { get; set; }
        public ICollection<ChatMessage> ReceivedMessages { get; set; }
        public ICollection<Review> ReviewsGiven { get; set; }
        public ICollection<Review> ReviewsReceived { get; set; }
        public ICollection<Referral> ReferralsSent { get; set; }
        public ICollection<Referral> ReferralsReceived { get; set; }
        public ICollection<Banner> banners { get; set; }
    }
}
