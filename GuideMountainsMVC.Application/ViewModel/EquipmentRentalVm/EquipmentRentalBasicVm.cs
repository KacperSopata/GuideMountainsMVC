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
    public class EquipmentRentalBasicVm : IMapFrom<EquipmentRental>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double PricePerDay { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<EquipmentRental, EquipmentRentalBasicVm>();
        }
    }
}