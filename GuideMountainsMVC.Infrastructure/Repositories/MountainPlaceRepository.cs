using GuideMountainsMVC.Domain.Model;
using GuideMountainsMVC.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            var mountainPlace = _context.MountainPlaces
                .Include(mp => mp.Accommodations)
                .Include(mp => mp.SkiPasses)
                .Include(mp => mp.EquipmentRentals)
                .FirstOrDefault(mp => mp.Id == id);

            if (mountainPlace != null)
            {
                // Usuń ręcznie zależne encje
                _context.Accommodations.RemoveRange(mountainPlace.Accommodations);
                _context.SkiPasses.RemoveRange(mountainPlace.SkiPasses);
                _context.EquipmentRentals.RemoveRange(mountainPlace.EquipmentRentals);

                // Teraz usuwamy MountainPlace
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
