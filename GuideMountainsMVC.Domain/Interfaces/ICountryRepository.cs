using GuideMountainsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Domain.Interfaces
{
    public interface ICountryRepository
    {
        public List<Country> GetAllCountries();
        int AddCountry(Country country);
        Country GetDetail(int id);
        void DeleteCountry(Country country);
        void UpdateCountry(Country country);
        Country GetCountryById(int id);
    }
}
