using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.ViewModel.AccommodationVm
{
    public class AccommodationForListVm : IMapFrom<Accommodation>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string MountainPlaceName { get; set; }
        public int CountryId { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Accommodation, AccommodationForListVm>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src =>
                    src.Description.Length > 50 ? src.Description.Substring(0, 50) + "..." : src.Description))
                .ForMember(dest => dest.MountainPlaceName, opt => opt.MapFrom(src => src.MountainPlace.Name));
        }
    }
}