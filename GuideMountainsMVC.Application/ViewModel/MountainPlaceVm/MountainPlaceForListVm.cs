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
    public class MountainPlaceForListVm : IMapFrom<MountainPlace>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string CountryName { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }


        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<MountainPlace, MountainPlaceForListVm>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                    src.Description.Length > 50 ? src.Description.Substring(0, 50) + "..." : src.Description))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));
        }
    }
}