using GuideMountainsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Interfaces
{
    public interface IAccommodationRepository
    {
        IQueryable<Accommodation> GetAllAccommodations();
        int AddAccommodation(Accommodation accommodation);
        Accommodation GetDetail(int id);
        void DeleteAccommodation(Accommodation accommodation);
        void UpdateAccommodation(Accommodation accommodation);
        Accommodation GetAccommodationById(int id);
        IEnumerable<Accommodation> GetByCountryId(int countryId);

    }
}
