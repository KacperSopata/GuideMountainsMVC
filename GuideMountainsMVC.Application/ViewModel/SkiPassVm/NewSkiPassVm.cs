using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuideMountainsMVC.Application.ViewModel.SkiPassVm
{
    public class NewSkiPassVm : IMapFrom<SkiPass>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public IFormFile ImageContent { get; set; }
        public int MountainPlaceId { get; set; }
        public int SkiPassTypeId { get; set; }
        public int CountryId { get; set; }
        public double BasePrice { get; set; }

        public List<SelectListItem> Countries { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> MountainPlaces { get; set; } = new List<SelectListItem>();

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<SkiPass, NewSkiPassVm>()
    .ForMember(dest => dest.MountainPlaceId, opt => opt.MapFrom(src => src.MountainPlaceId))
    .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
    .ForMember(dest => dest.BasePrice, opt => opt.MapFrom(src => src.BasePrice)); // 🔧 DODAJ TO

            profile.CreateMap<NewSkiPassVm, SkiPass>()
                .ForMember(dest => dest.MountainPlaceId, opt => opt.MapFrom(src => src.MountainPlaceId))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
                .ForMember(dest => dest.BasePrice, opt => opt.MapFrom(src => src.BasePrice)) // 🔧 I TU TEŻ
                .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}
