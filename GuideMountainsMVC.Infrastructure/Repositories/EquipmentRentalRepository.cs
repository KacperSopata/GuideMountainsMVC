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
    public class EquipmentRentalRepository : IEquipmentRentalRepository
    {
        private readonly GuideMountainsMVCDbContext _context;

        public EquipmentRentalRepository(GuideMountainsMVCDbContext context)
        {
            _context = context;
        }

        public IQueryable<EquipmentRental> GetAllEquipmentRentals()
        {
            return _context.EquipmentRentals.AsQueryable();
        }

        public int AddEquipmentRental(EquipmentRental equipmentRental)
        {
            _context.EquipmentRentals.Add(equipmentRental);
            _context.SaveChanges();
            return equipmentRental.Id;
        }

        public EquipmentRental GetDetail(int id)
        {
            return _context.EquipmentRentals.FirstOrDefault(er => er.Id == id);
        }

        public void DeleteEquipmentRental(EquipmentRental equipmentRental)
        {
            _context.EquipmentRentals.Remove(equipmentRental);
            _context.SaveChanges();
        }

        public void UpdateEquipmentRental(EquipmentRental equipmentRental)
        {
            _context.EquipmentRentals.Update(equipmentRental);
            _context.SaveChanges();
        }

        public EquipmentRental GetEquipmentRentalById(int id)
        {
            return _context.EquipmentRentals
                           .Include(er => er.CategoryEquipmentRental)
                           .Include(er => er.Country)               // 👈 dodaj to
                           .Include(er => er.MountainPlace)         // 👈 i to
                           .FirstOrDefault(er => er.Id == id);
        }


        public IEnumerable<EquipmentRental> GetByCategoryId(int categoryForeignKey)
        {
            return _context.EquipmentRentals.Where(er => er.CategoryEquipmentRentalId == categoryForeignKey);
        }
        public IEnumerable<CategoryEquipmentRental> GetAllCategories()
        {
            // Jeśli używasz bazy danych, np. Entity Framework:
            return _context.CategoryEquipmentRentals.ToList();
        }

        public IEnumerable<EquipmentRental> GetByCountryId(int countryForeignKey)
        {
            return _context.EquipmentRentals.Where(mp => mp.CountryId == countryForeignKey);
        }
        public IEnumerable<EquipmentRental> GetEquipmentRentalsByMountainPlaceId(int mountainPlaceId)
        {
            return _context.EquipmentRentals
                .Include(er => er.CategoryEquipmentRental)  // 👈 DODAJ TO
                .Include(er => er.MountainPlace)            // 👈 DOBRZE TEŻ MIEĆ
                .Include(er => er.Country)                  // 👈 JEŚLI POTRZEBUJESZ
                .Where(e => e.MountainPlaceId == mountainPlaceId)
                .ToList();
        }

        // EquipmentRentalRepository.cs
        public EquipmentRental GetById(int id)
        {
            return _context.EquipmentRentals.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<MountainPlace> GetMountainPlacesByCountryId(int countryId)
        {
            return _context.MountainPlaces
                .Where(mp => mp.CountryId == countryId)
                .ToList();
        }

    }
}
