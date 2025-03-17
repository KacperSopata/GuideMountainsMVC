using GuideMountainsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Interfaces
{
    public interface IMountainPlaceRepository
    {
        IQueryable<MountainPlace> GetAll();

        // Pobiera jedno miejsce górskie po ID
        MountainPlace GetById(int id);

        // Dodaje nowe miejsce górskie
        int Add(MountainPlace mountainPlace);
        // Aktualizuje istniejące miejsce górskie
        void Update(MountainPlace mountainPlace);

        // Usuwa miejsce górskie
        void Delete(int id);
        IEnumerable<MountainPlace> GetByCountryId(int countryId);
    }
}
