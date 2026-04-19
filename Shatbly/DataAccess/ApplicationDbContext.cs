using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shatbly.Models;

namespace Shatbly.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
     
        public DbSet<User> Users { get; set; }
        public DbSet<OTP_Verification> OtpVerifications { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<RefereshToken> RefreshTokens { get; set; }
        public DbSet<DeviceToken> DeviceTokens { get; set; }
        public DbSet<WorkerProfile> WorkerProfiles { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<WorkerService> WorkerServices { get; set; }
        public DbSet<Avalability> Availabilities { get; set; }
        public DbSet<UnAvalability> Unavailabilities { get; set; }
        public DbSet<PortfolioMedia> PortfolioMediaItems { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingItem> BookingItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SupportTicket> SupportTickets { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionCode> PromotionCodes { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<LogActivity> LogActivities { get; set; }
        public DbSet<Dipuste> Disputes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email).IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Phone).IsUnique();
            modelBuilder.Entity<RefereshToken>()
                .HasIndex(r => r.Token).IsUnique();
            modelBuilder.Entity<DeviceToken>()
                .HasIndex(d => d.Token).IsUnique();
            modelBuilder.Entity<Coupon>()
                .HasIndex(c => c.Code).IsUnique();
            modelBuilder.Entity<PromotionCode>()
                .HasIndex(pc => pc.Code).IsUnique();
            modelBuilder.Entity<Referral>()
                .HasIndex(r => r.Code).IsUnique();
            modelBuilder.Entity<Payment>()
                .HasIndex(p => p.BookingId).IsUnique();
            modelBuilder.Entity<Wallet>()
                .HasIndex(w => w.UserId).IsUnique();
            modelBuilder.Entity<WorkerProfile>()
                .HasOne(wp => wp.User)
                .WithOne(u => u.WorkerProfile)
                .HasForeignKey<WorkerProfile>(wp => wp.UserId);
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Client)
                .WithMany(u => u.ClientBookings)
                .HasForeignKey(b => b.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Worker)
                .WithMany(wp => wp.WorkerBookings)
                .HasForeignKey(b => b.WorkerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reviewer)
                .WithMany(u => u.ReviewsGiven)
                .HasForeignKey(r => r.ReviewerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Reviewee)
                .WithMany(u => u.ReviewsReceived)
                .HasForeignKey(r => r.RevieweeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Dipuste>()
                .HasOne(d => d.RaisedBy)
                .WithMany()
                .HasForeignKey(d => d.RaisedById)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Dipuste>()
                .HasOne(d => d.Against)
                .WithMany()
                .HasForeignKey(d => d.AgainstId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Referral>()
                .HasOne(r => r.Referrer)
                .WithMany(u => u.ReferralsSent)
                .HasForeignKey(r => r.ReferrerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Referral>()
                .HasOne(r => r.Referred)
                .WithMany(u => u.ReferralsReceived)
                .HasForeignKey(r => r.ReferredId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Client)
                .WithMany()
                .HasForeignKey(f => f.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Favorite>()
             .HasOne(f => f.Worker)
             .WithMany()
             .HasForeignKey(f => f.WorkerId)
             .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Favorite>()
                .HasIndex(f => new { f.ClientId, f.WorkerId }).IsUnique();
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Coupon)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CouponId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.PromoCode)
                .WithMany(pc => pc.Bookings)
                .HasForeignKey(b => b.PromoCodeId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
