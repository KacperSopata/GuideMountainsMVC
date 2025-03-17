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

        public GuideMountainsMVCDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            // Konfiguracja relacji dla SkiPass z Countries (jeśli dotyczy)
            modelBuilder.Entity<SkiPass>()
                .HasOne(sp => sp.Country)
                .WithMany(c => c.SkiPasses)
                .HasForeignKey(sp => sp.CountryId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
