using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Application.ViewModel.SkiPassVm
{
    public class SkiPassTypeVm : IMapFrom<SkiPassType>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double PriceMultiplier { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<SkiPassType, SkiPassTypeVm>();
        }
    }
}
