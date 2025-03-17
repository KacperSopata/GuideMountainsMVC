using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Application.ViewModel.SkiPassVm;
using GuideMountainsMVC.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.ViewModel.AccommodationVm
{
    public class NewAccommodationVm : IMapFrom<Accommodation>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public IFormFile ImageContent { get; set; }
        public int MountainPlaceId { get; set; } // Zmieniono z MountainPlaceId
        public int CountryId { get; set; }
        public List<SelectListItem> Countries { get; set; } = new List<SelectListItem>(); // Inicjalizacja listy        public byte[] Image { get; set; }
        public List<SelectListItem> MountainPlaces { get; set; } = new List<SelectListItem>();

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Accommodation, NewAccommodationVm>()
                .ForMember(dest => dest.MountainPlaceId, opt => opt.MapFrom(src => src.MountainPlaceId))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId));

            profile.CreateMap<NewAccommodationVm, Accommodation>()
                .ForMember(dest => dest.MountainPlaceId, opt => opt.MapFrom(src => src.MountainPlaceId))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId));
        }
    }
}