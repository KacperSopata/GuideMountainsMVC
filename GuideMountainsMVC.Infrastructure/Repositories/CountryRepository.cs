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
    public class CountryRepository : ICountryRepository
    {
        private readonly GuideMountainsMVCDbContext _context;

        public CountryRepository(GuideMountainsMVCDbContext context)
        {
            _context = context;
        }

        public List<Country> GetAllCountries()
        {
            return _context.Countries.ToList(); // Pobieramy wszystkie kraje z bazy
        }

        public int AddCountry(Country country)
        {
            _context.Countries.Add(country);
            _context.SaveChanges();
            return country.Id;
        }

        public Country GetDetail(int id)
        {
            return _context.Countries.FirstOrDefault(c => c.Id == id);
        }

        public void DeleteCountry(Country country)
        {
            _context.Countries.Remove(country);
            _context.SaveChanges();
        }
        public void UpdateCountry(Country country)
        {
            _context.Countries.Update(country);
            _context.SaveChanges();
        }
        public Country GetCountryById(int id)
        {
            return _context.Countries
                           .Include(c => c.MountainPlaces) // Przykład, jeśli potrzebujesz powiązane obiekty
                           .Include(c => c.SkiPasses)
                           .Include(c => c.Accommodations)
                           .FirstOrDefault(c => c.Id == id);
        }
    }
}