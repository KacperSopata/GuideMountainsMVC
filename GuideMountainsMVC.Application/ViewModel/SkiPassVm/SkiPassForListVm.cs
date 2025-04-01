using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.ViewModel.SkiPassVm
{
    public class SkiPassForListVm : IMapFrom<SkiPass>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public byte[] Image { get; set; }
        public string MountainPlaceName { get; set; }
        public string CountryName { get; set; }
        public string SkiPassTypeName { get; set; } // Nowe pole

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<SkiPass, SkiPassForListVm>()
                .ForMember(dest => dest.MountainPlaceName, opt => opt.MapFrom(src => src.MountainPlace.Name))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
                .ForMember(dest => dest.SkiPassTypeName, opt => opt.MapFrom(src => src.SkiPassType.Name))
                .ForMember(dest => dest.SkiPassTypeName, opt => opt.MapFrom(src => src.SkiPassType.Name));
        }
    }
}