﻿using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.ViewModel.AccommodationVm
{
    public class AccommodationDetailVm : IMapFrom<Accommodation>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string MountainPlaceName { get; set; }
        public double PricePerNight { get; set; }
        public string CountryName { get; set; }
        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Accommodation, AccommodationDetailVm>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name))
                .ForMember(dest => dest.MountainPlaceName, opt => opt.MapFrom(src => src.MountainPlace.Name))
                .ForMember(dest => dest.PricePerNight, opt => opt.MapFrom(src => src.PricePerNight));
        }

    }
}