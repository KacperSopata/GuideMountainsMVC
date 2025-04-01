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
    public class SkiPassRepository : ISkiPassRepository
    {
        private readonly GuideMountainsMVCDbContext _context;

        public SkiPassRepository(GuideMountainsMVCDbContext context)
        {
            _context = context;
        }

        public IQueryable<SkiPass> GetAllSkiPasses()
        {
            return _context.SkiPasses.AsQueryable();
        }

        public int AddSkiPass(SkiPass skiPass)
        {
            _context.SkiPasses.Add(skiPass);
            _context.SaveChanges();
            return skiPass.Id;
        }

        public SkiPass GetDetail(int id)
        {
            return _context.SkiPasses.FirstOrDefault(sp => sp.Id == id);
        }

        public void DeleteSkiPass(SkiPass skiPass)
        {
            _context.SkiPasses.Remove(skiPass);
            _context.SaveChanges();
        }

        public void UpdateSkiPass(SkiPass skiPass)
        {
            _context.SkiPasses.Update(skiPass);
            _context.SaveChanges();
        }

        public SkiPass GetSkiPassById(int id)
        {
            return _context.SkiPasses
                           .Include(sp => sp.MountainPlace) // Przykład, jeśli potrzebujesz powiązane obiekty
                           .Include(sp => sp.Country)
                           .FirstOrDefault(sp => sp.Id == id);
        }
        public IEnumerable<SkiPass> GetByCountryId(int countryForeignKey)
        {
            return _context.SkiPasses.Where(mp => mp.CountryId == countryForeignKey);
        }
        public IQueryable<SkiPassType> GetAllSkiPassTypes()
        {
            return _context.SkiPassTypes;
        }
        // SkiPassTypeRepository.cs
        public SkiPassType GetById(int id)
        {
            return _context.SkiPassTypes.FirstOrDefault(x => x.Id == id);
        }
        public IEnumerable<SkiPass> GetByMountainPlaceId(int mountainPlaceId)
        {
            return _context.SkiPasses
                .Include(s => s.MountainPlace)
                .Include(s => s.Country)
                .Include(s => s.SkiPassType) // ✅ to jest kluczowe!
                .Where(s => s.MountainPlaceId == mountainPlaceId)
                .ToList();
        }


    }
}