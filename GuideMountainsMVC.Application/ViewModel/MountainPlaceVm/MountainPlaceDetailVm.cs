using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.ViewModel.MountainPlaceVm
{
    public class MountainPlaceDetailVm : IMapFrom<MountainPlace>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string CountryName { get; set; }
        public int CountryId { get; set; }

        public List<string> SkiPassNames { get; set; }
        public List<string> AccommodationNames { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<MountainPlace, MountainPlaceDetailVm>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
                .ForMember(dest => dest.AccommodationNames, opt => opt.MapFrom(src => src.Accommodations.Select(a => a.Name)));
        }
    }
}