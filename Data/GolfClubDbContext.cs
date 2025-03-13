using Microsoft.EntityFrameworkCore;
using PortmarnockGolfClub.Models;

namespace PortmarnockGolfClub.Data
{
    public class GolfClubDbContext : DbContext
    {
        public GolfClubDbContext(DbContextOptions<GolfClubDbContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Booking Configuration
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.BookingID); // Primary key
                entity.Property(b => b.MembershipNumber).IsRequired(); // Foreign key linking to Member
                entity.Property(b => b.Date).IsRequired(); // Booking date
                entity.Property(b => b.TimeSlot).IsRequired(); // Tee time slot
                entity.Property(b => b.PlayerDetails).HasMaxLength(1000); // Optional: Define max length for player details

                // Define relationship: A Member can have multiple Bookings
                entity.HasOne<Member>()
                      .WithMany()
                      .HasForeignKey(b => b.MembershipNumber)
                      .OnDelete(DeleteBehavior.Cascade); // Cascade delete bookings if member is deleted

                entity.HasIndex(b => b.TimeSlot); // Optimize queries for tee time slots
            });

            // Member Configuration
            modelBuilder.Entity<Member>(entity =>
            {
                entity.HasKey(m => m.MembershipNumber); // Primary key
                entity.Property(m => m.Name).IsRequired().HasMaxLength(255); // Name is required and has a max length
                entity.Property(m => m.Email).IsRequired(); // Email is required
                entity.Property(m => m.Gender).IsRequired(); // Gender is required
                entity.Property(m => m.Handicap).IsRequired(); // Handicap is required

                entity.HasIndex(m => m.Handicap); // Optimize queries based on handicap range
            });
        }
    }
}