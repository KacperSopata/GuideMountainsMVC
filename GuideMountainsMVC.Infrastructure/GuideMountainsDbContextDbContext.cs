using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GuideMountainsMVC.Domain.Model;
using Microsoft.AspNetCore.Identity;

namespace GuideMountainsMVC.Infrastructure
{
    public class GuideMountainsMVCDbContext : IdentityDbContext
    {
        public DbSet<MountainPlace> MountainPlaces { get; set; }
        public DbSet<SkiPass> SkiPasses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Accommodation> Accommodations { get; set; }
        public DbSet<EquipmentRental> EquipmentRentals { get; set; }
        public DbSet<CategoryEquipmentRental> CategoryEquipmentRentals { get; set; }
        public DbSet<SkiPassType> SkiPassTypes { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationItem> ReservationItems { get; set; }
        public DbSet<AccommodationReservation> AccommodationReservations { get; set; }

        public GuideMountainsMVCDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<Accommodation>()
            //    .HasOne(a => a.Country)
            //    .WithMany(c => c.Accommodations)
            //    .HasForeignKey(a => a.CountryId)
            //    .OnDelete(DeleteBehavior.Restrict);  // Brak akcji na usuwanie

            //modelBuilder.Entity<Accommodation>()
            //    .HasOne(a => a.MountainPlace)
            //    .WithMany(m => m.Accommodations)
            //    .HasForeignKey(a => a.MountainPlaceId)
            //    .OnDelete(DeleteBehavior.Restrict);  // Brak akcji na usuwanie

            //modelBuilder.Entity<SkiPass>()
            //    .HasOne(sp => sp.MountainPlace)
            //    .WithMany(mp => mp.SkiPasses)
            //    .HasForeignKey(sp => sp.MountainPlaceId)
            //    .OnDelete(DeleteBehavior.Restrict);  // Brak kaskadowego usuwania

            //// Konfiguracja relacji dla SkiPass z Countries (jeśli dotyczy)
            //modelBuilder.Entity<SkiPass>()
            //    .HasOne(sp => sp.Country)
            //    .WithMany(c => c.SkiPasses)
            //    .HasForeignKey(sp => sp.CountryId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Accommodation>()
                         .HasOne(a => a.Country)
                         .WithMany(c => c.Accommodations)
                         .HasForeignKey(a => a.CountryId)
                        .OnDelete(DeleteBehavior.Restrict);  // Brak akcji na usuwanie

            modelBuilder.Entity<Accommodation>()
                .HasOne(a => a.MountainPlace)
                .WithMany(m => m.Accommodations)
                .HasForeignKey(a => a.MountainPlaceId)
                .OnDelete(DeleteBehavior.Restrict);  // Brak akcji na usuwanie

            modelBuilder.Entity<SkiPass>()
                .HasOne(sp => sp.MountainPlace)
                .WithMany(mp => mp.SkiPasses)
                .HasForeignKey(sp => sp.MountainPlaceId)
                .OnDelete(DeleteBehavior.Restrict);  // Brak kaskadowego usuwania

            modelBuilder.Entity<SkiPass>()
                .HasOne(sp => sp.Country)
                .WithMany(c => c.SkiPasses)
                .HasForeignKey(sp => sp.CountryId)
                .OnDelete(DeleteBehavior.Restrict);  // Brak kaskadowego usuwania

            modelBuilder.Entity<EquipmentRental>()
                .HasOne(e => e.MountainPlace)
                .WithMany(mp => mp.EquipmentRentals)
                .HasForeignKey(e => e.MountainPlaceId)
                .OnDelete(DeleteBehavior.NoAction);  // Wyłącz cascade

            modelBuilder.Entity<EquipmentRental>()
                .HasOne(e => e.Country)
                .WithMany(c => c.EquipmentRentals)
                .HasForeignKey(e => e.CountryId)
                .OnDelete(DeleteBehavior.NoAction);  // Wyłącz cascade

            modelBuilder.Entity<EquipmentRental>()
                 .HasOne(e => e.CategoryEquipmentRental)
                 .WithMany(c => c.EquipmentRentals)
                 .HasForeignKey(e => e.CategoryEquipmentRentalId)
                 .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ReservationItem>()
                .HasOne(ri => ri.Reservation)
                .WithMany(r => r.ReservationItems)
                .HasForeignKey(ri => ri.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReservationItem>()
                .HasOne(ri => ri.Accommodation)
                .WithMany()
                .HasForeignKey(ri => ri.AccommodationId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ReservationItem>()
                .HasOne(ri => ri.SkiPass)
                .WithMany()
                .HasForeignKey(ri => ri.SkiPassId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ReservationItem>()
                .HasOne(ri => ri.EquipmentRental)
                .WithMany()
                .HasForeignKey(ri => ri.EquipmentRentalId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<AccommodationReservation>(entity =>
            {
                entity.HasOne(ar => ar.Accommodation)
                      .WithMany()
                      .HasForeignKey(ar => ar.AccommodationId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SkiPassType>().HasData(
               new SkiPassType { Id = 1, Name = "Normalny", PriceMultiplier = 1.0 },
               new SkiPassType { Id = 2, Name = "Ulgowy", PriceMultiplier = 0.8 }
);
        }
    }
}