using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.ViewModel.CountryVm
{
    public class NewCountryVm : IMapFrom<Country>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<Country, NewCountryVm>();
            profile.CreateMap<NewCountryVm, Country>();
        }
    }
}