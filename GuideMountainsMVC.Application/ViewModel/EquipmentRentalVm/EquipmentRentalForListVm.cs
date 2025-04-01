using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Application.ViewModel.EquipmentRentalVm
{
    public class EquipmentRentalForListVm : IMapFrom<EquipmentRental>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double PricePerDay { get; set; }
        public int CountryId { get; set; }
        public int CategoryId { get; set; }
        public byte[] Image { get; set; }
        public string CategoryName { get; set; }
        public string Name { get; set; }
        public string MountainPlaceName { get; set; }
        public string CountryName { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<EquipmentRental, EquipmentRentalForListVm>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryEquipmentRental.Name))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PricePerDay, opt => opt.MapFrom(src => src.PricePerDay))
                .ForMember(dest => dest.MountainPlaceName, opt => opt.MapFrom(src => src.MountainPlace.Name))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));
        }
    }
}