using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Infrastructure.Repositories
{
    public class SkiPassTypeRepository : ISkiPassTypeRepository
    {
        private readonly GuideMountainsMVCDbContext _context;

        public SkiPassTypeRepository(GuideMountainsMVCDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SkiPassType> GetAll()
        {
            return _context.SkiPassTypes.ToList();
        }

        public SkiPassType GetById(int id)
        {
            return _context.SkiPassTypes.FirstOrDefault(x => x.Id == id);
        }
    }
}
