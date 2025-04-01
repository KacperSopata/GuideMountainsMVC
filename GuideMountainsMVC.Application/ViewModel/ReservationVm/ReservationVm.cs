using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Domain.Model;
using System;
using System.Collections.Generic;

namespace GuideMountainsMVC.Application.ViewModel.ReservationVm
{
    public class ReservationVm : IMapFrom<Reservation>
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string MountainPlaceName { get; set; }
        public bool IsConfirmed { get; set; }
        public List<ReservationItemVm> ReservationItems { get; set; } = new List<ReservationItemVm>();

        public void ConfigureMapping(Profile profile)
        {
            // Mapowanie z encji do VM (do widoku)
            profile.CreateMap<Reservation, ReservationVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.IsConfirmed, opt => opt.MapFrom(src => src.IsConfirmed))
                .ForMember(dest => dest.MountainPlaceName, opt => opt.MapFrom(src => src.MountainPlaceName))
                .ForMember(dest => dest.ReservationItems, opt => opt.MapFrom(src => src.ReservationItems));

            // Mapowanie z VM do encji (do zapisu)
            profile.CreateMap<ReservationVm, Reservation>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.IsConfirmed, opt => opt.MapFrom(src => src.IsConfirmed))
                .ForMember(dest => dest.MountainPlaceName, opt => opt.MapFrom(src => src.MountainPlaceName))
                .ForMember(dest => dest.ReservationItems, opt => opt.MapFrom(src => src.ReservationItems));

            // Mapowania dla pozycji rezerwacji
            profile.CreateMap<ReservationItemVm, ReservationItem>();
            profile.CreateMap<ReservationItem, ReservationItemVm>();
        }


    }
}
