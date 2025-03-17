using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.ViewModel.CountryVm
{
    public class CountryDetailVm : IMapFrom<Country>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> MountainPlaceNames { get; set; }
        public List<string> SkiPassNames { get; set; }
        public List<string> AccommodationNames { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Country, CountryDetailVm>()
                .ForMember(dest => dest.MountainPlaceNames, opt => opt.MapFrom(src => src.MountainPlaces.Select(mp => mp.Name)))
                .ForMember(dest => dest.AccommodationNames, opt => opt.MapFrom(src => src.Accommodations.Select(a => a.Name)));
        }
    }
}