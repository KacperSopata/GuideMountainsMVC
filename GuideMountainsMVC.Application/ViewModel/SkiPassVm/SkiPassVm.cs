using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Application.ViewModel.SkiPassVm
{
    public class SkiPassVm : IMapFrom<SkiPass>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double BasePrice { get; set; } // Dodane pole do pobierania ceny
        public int MountainPlaceId { get; set; }
        public int CountryId { get; set; }
        public int SkiPassTypeId { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<SkiPass, SkiPassVm>();
        }
    }
}