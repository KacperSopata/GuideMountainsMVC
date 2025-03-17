using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace GuideMountainsMVC.Application.ViewModel.SkiPassVm
{
    public class NewSkiPassVm : IMapFrom<SkiPass>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public IFormFile ImageContent { get; set; }
        public int MountainPlaceId { get; set; } // Zmieniono z MountainPlaceId
        public int CountryId { get; set; }
        public List<SelectListItem> Countries { get; set; } = new List<SelectListItem>(); // Inicjalizacja listy        public byte[] Image { get; set; }
        public List<SelectListItem> MountainPlaces { get; set; } = new List<SelectListItem>();
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<SkiPass, NewSkiPassVm>()
                .ForMember(dest => dest.MountainPlaceId, opt => opt.MapFrom(src => src.MountainPlaceId))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId));

            profile.CreateMap<NewSkiPassVm, SkiPass>()
                .ForMember(dest => dest.MountainPlaceId, opt => opt.MapFrom(src => src.MountainPlaceId))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId));
        }
    }
}
