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
    public class SkiPassDetailVm : IMapFrom<SkiPass>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string MountainPlaceName { get; set; }
        public string CountryName { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<SkiPass, SkiPassDetailVm>()
                .ForMember(dest => dest.MountainPlaceName, opt => opt.MapFrom(src => src.MountainPlace.Name))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));
        }
    }
}