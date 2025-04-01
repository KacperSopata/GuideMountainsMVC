using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Application.ViewModel.SkiPassVm;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Application.ViewModel.EquipmentRentalVm
{
    public class EquipmentRentalDetailVm : IMapFrom<EquipmentRental>
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string CategoryName { get; set; }
        public string MountainPlaceName { get; set; }
        public string CountryName { get; set; }
        public double PricePerDay { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<EquipmentRental, EquipmentRentalDetailVm>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.CategoryEquipmentRental.Name))
                .ForMember(dest => dest.MountainPlaceName, opt => opt.MapFrom(src => src.MountainPlace.Name))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
                .ForMember(dest => dest.PricePerDay, opt => opt.MapFrom(src => src.PricePerDay)); // 🔥 TO DODAJ!
        }
    }

}
