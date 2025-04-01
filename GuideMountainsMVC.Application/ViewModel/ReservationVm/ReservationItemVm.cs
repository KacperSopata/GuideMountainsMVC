using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Application.ViewModel.ReservationVm
{
    public class ReservationItemVm : IMapFrom<ReservationItem>
    {
        public int Id { get; set; }
        public int? SkiPassId { get; set; }
        public string SkiPassName { get; set; }
        public string SkiPassTypeName { get; set; }
        public int? SkiPassDays { get; set; }
        public int? SkiPassQuantity { get; set; }
        public int? AccommodationId { get; set; }
        public string AccommodationName { get; set; }
        public DateTime? AccommodationStartDate { get; set; }   // ← DODAJ TO
        public DateTime? AccommodationEndDate { get; set; }     // ← DODAJ TO
        public int? AccommodationGuests { get; set; }
        public int? AccommodationNights { get; set; }
        public int? EquipmentRentalId { get; set; }
        public string EquipmentName { get; set; }
        public int? EquipmentQuantity { get; set; }
        public int? EquipmentDays { get; set; }

        public double Price { get; set; }
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<ReservationItem, ReservationItemVm>()
                .ForMember(dest => dest.SkiPassName, opt => opt.MapFrom(src => src.SkiPass.Description))
                .ForMember(dest => dest.SkiPassTypeName, opt => opt.MapFrom(src => src.SkiPass.SkiPassType.Name))
                .ForMember(dest => dest.AccommodationName, opt => opt.MapFrom(src => src.Accommodation.Name))
                .ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(src => src.EquipmentRental.Name))
                .ForMember(dest => dest.AccommodationStartDate, opt => opt.MapFrom(src => src.AccommodationStartDate))
                .ForMember(dest => dest.AccommodationEndDate, opt => opt.MapFrom(src => src.AccommodationEndDate))
                .ForMember(dest => dest.AccommodationGuests, opt => opt.MapFrom(src => src.AccommodationGuests))
                .ForMember(dest => dest.AccommodationNights, opt => opt.MapFrom(src => src.AccommodationNights))
                .ForMember(dest => dest.SkiPassDays, opt => opt.MapFrom(src => src.SkiPassDays))
                .ForMember(dest => dest.SkiPassQuantity, opt => opt.MapFrom(src => src.SkiPassQuantity))
                .ForMember(dest => dest.SkiPassTypeName, opt => opt.MapFrom(src => src.SkiPass.SkiPassType.Name))
                .ForMember(dest => dest.EquipmentQuantity, opt => opt.MapFrom(src => src.EquipmentQuantity))
                .ForMember(dest => dest.EquipmentName, opt => opt.MapFrom(src => src.EquipmentRental.Name))
                .ForMember(dest => dest.EquipmentDays, opt => opt.MapFrom(src => src.EquipmentDays));
        }

    }
}
