using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Models;
using TravelAgency.Models.UserRelated;
using TravelAgencyWeb.Models;

namespace TravelAgencyWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext<AccountModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FlightModel> Flights { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookingModel>()
                .HasKey(b => b.BookingId);
            
            modelBuilder.Entity<BookingModel>()
                .Property(b => b.DepartureTime)
                .HasColumnType("date");
            
            modelBuilder.Entity<BookingModel>()
                .Property(b => b.BookingDate)
                .HasColumnType("date");
            
            modelBuilder.Entity<BookingModel>()
                .Property(f => f.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<FlightModel>()
                .HasKey(f => f.FlightId);

            modelBuilder.Entity<FlightModel>()
                .Property(f => f.DepartureTime)
                .HasColumnType("date");

            modelBuilder.Entity<FlightModel>()
                .Property(f => f.ArrivalTime)
                .HasColumnType("date");

            modelBuilder.Entity<FlightModel>()
                .Property(f => f.Price)
                .HasColumnType("decimal(18, 2)");
        }
    }
}