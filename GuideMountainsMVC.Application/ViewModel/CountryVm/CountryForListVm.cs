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
    public class CountryForListVm : IMapFrom<Country>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MountainPlacesCount { get; set; }
        public int SkiPassesCount { get; set; }
        public int AccommodationsCount { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Country, CountryForListVm>()
                .ForMember(dest => dest.MountainPlacesCount, opt => opt.MapFrom(src => src.MountainPlaces.Count))
                .ForMember(dest => dest.SkiPassesCount, opt => opt.MapFrom(src => src.SkiPasses.Count))
                .ForMember(dest => dest.AccommodationsCount, opt => opt.MapFrom(src => src.Accommodations.Count));
        }
    }
}