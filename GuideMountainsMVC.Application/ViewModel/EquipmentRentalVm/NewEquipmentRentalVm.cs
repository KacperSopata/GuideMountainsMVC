using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GuideMountainsMVC.Application.ViewModel.EquipmentRentalVm
{
    public class NewEquipmentRentalVm : IMapFrom<EquipmentRental>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public IFormFile ImageContent { get; set; }
        public int MountainPlaceId { get; set; }
        public int CountryId { get; set; }
        public int CategoryId { get; set; }
        public double PricePerDay {  get; set; }
        public List<SelectListItem> Categories { get; set; } = new();
        public List<SelectListItem> Countries { get; set; } = new();
        public List<SelectListItem> MountainPlaces { get; set; } = new();

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<EquipmentRental, NewEquipmentRentalVm>()
                .ForMember(dest => dest.MountainPlaceId, opt => opt.MapFrom(src => src.MountainPlaceId))
                .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
                .ForMember(dest => dest.PricePerDay, opt => opt.MapFrom(src => src.PricePerDay))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryEquipmentRentalId))
                .ReverseMap();
        }
    }
}
