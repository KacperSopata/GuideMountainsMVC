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
    public class ReservationForListVm : IMapFrom<Reservation>
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string MountainPlaceName { get; set; }
        public bool IsConfirmed { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Reservation, ReservationForListVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.MountainPlaceName, opt => opt.MapFrom(src => src.MountainPlaceName))
                .ForMember(dest => dest.IsConfirmed, opt => opt.MapFrom(src => src.IsConfirmed));
        }
    }
}
