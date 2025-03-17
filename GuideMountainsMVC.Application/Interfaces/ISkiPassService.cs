using GuideMountainsMVC.Application.ViewModel.SkiPassVm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.Interfaces
{
    public interface ISkiPassService
    {
        ListSkiPassVm GetAllSkiPasses(int pageSize, int currentPage, string searchString);
        SkiPassDetailVm GetSkiPassDetail(int id);
        int AddSkiPass(NewSkiPassVm newSkiPass);
        void UpdateSkiPass(NewSkiPassVm updatedSkiPass);
        void DeleteSkiPass(int id);
        ListSkiPassVm GetSkiPassByCountryId(int countryForeignKey);
    }
}
