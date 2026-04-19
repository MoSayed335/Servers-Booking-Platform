using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Shatbly.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(150)]
        public string FName { get; set; }
        [Required]
        [MaxLength(150)]
        public string LName { get; set; }
        [Required]
                [Phone]
        public string Phone { get; set; }
        public string? Address { get; set; }

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
