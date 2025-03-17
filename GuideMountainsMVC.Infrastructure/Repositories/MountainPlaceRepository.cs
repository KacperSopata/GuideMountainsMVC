using GuideMountainsMVC.Domain.Model;
using GuideMountainsMVC.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GuideMountainsMVC.Infrastructure.Repositories
{
    public class MountainPlaceRepository : IMountainPlaceRepository
    {
        private readonly GuideMountainsMVCDbContext _context;

        public MountainPlaceRepository(GuideMountainsMVCDbContext context)
        {
            _context = context;
        }

        // Pobierz wszystkie miejsca górskie
        public IQueryable<MountainPlace> GetAll()
        {
            return _context.MountainPlaces.AsQueryable();
        }

        // Pobierz miejsce górskie po ID
        public MountainPlace GetById(int id)
        {
            return _context.MountainPlaces.SingleOrDefault(mp => mp.Id == id);
        }

        // Dodaj miejsce górskie
        public int Add(MountainPlace mountainPlace)
        {
            _context.MountainPlaces.Add(mountainPlace);
            _context.SaveChanges();
            return mountainPlace.Id;

        }

        // Aktualizuj miejsce górskie
        public void Update(MountainPlace mountainPlace)
        {
            _context.MountainPlaces.Update(mountainPlace);
            _context.SaveChanges();
        }

        // Usuń miejsce górskie
        public void Delete(int id)
        {
            var mountainPlace = _context.MountainPlaces.SingleOrDefault(mp => mp.Id == id);
            if (mountainPlace != null)
            {
                _context.MountainPlaces.Remove(mountainPlace);
                _context.SaveChanges();
            }
        }

        // Pobierz miejsca górskie powiązane z danym krajem
        public IEnumerable<MountainPlace> GetByCountryId(int countryId)
        {
            return _context.MountainPlaces.Where(mp => mp.CountryId == countryId);
        }
    }
}
