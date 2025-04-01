using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Infrastructure.Repositories
{

    public class AccommodationRepository : IAccommodationRepository
    {
        private readonly GuideMountainsMVCDbContext _context;

        public AccommodationRepository(GuideMountainsMVCDbContext context)
        {
            _context = context;
        }

        public IQueryable<Accommodation> GetAllAccommodations()
        {
            return _context.Accommodations.AsQueryable();
        }

        public int AddAccommodation(Accommodation accommodation)
        {
            _context.Accommodations.Add(accommodation);
            _context.SaveChanges();
            return accommodation.Id;
        }

        public Accommodation GetDetail(int id)
        {
            return _context.Accommodations.FirstOrDefault(a => a.Id == id);
        }

        public void DeleteAccommodation(Accommodation accommodation)
        {
            _context.Accommodations.Remove(accommodation);
            _context.SaveChanges();
        }
        public void UpdateAccommodation(Accommodation accommodation)
        {
            _context.Accommodations.Update(accommodation);
            _context.SaveChanges();
        }
        public Accommodation GetAccommodationById(int id)
        {
            return _context.Accommodations
                           .Include(a => a.MountainPlace)
                            .Include(a => a.Country)// Przykład, jeśli potrzebujesz powiązane obiekty
                           .FirstOrDefault(a => a.Id == id);
        }
        public IEnumerable<Accommodation> GetByCountryId(int countryId)
        {
            return _context.Accommodations.Where(mp => mp.CountryId == countryId);
        }
        public IEnumerable<Accommodation> GetAccommodationsByMountainPlaceId(int mountainPlaceId)
        {
            return _context.Accommodations
                           .Where(a => a.MountainPlaceId == mountainPlaceId)
                           .ToList();
        }
    }
}