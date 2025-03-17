using GuideMountainsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Interfaces
{
    public interface ISkiPassRepository
    {
        IQueryable<SkiPass> GetAllSkiPasses();
        int AddSkiPass(SkiPass skiPass);
        SkiPass GetDetail(int id);
        void DeleteSkiPass(SkiPass skiPass);
        void UpdateSkiPass(SkiPass skiPass);
        SkiPass GetSkiPassById(int id);
        IEnumerable<SkiPass> GetByCountryId(int countryForeignKey);
    }
}
