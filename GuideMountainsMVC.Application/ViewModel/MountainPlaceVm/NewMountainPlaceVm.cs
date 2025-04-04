﻿using AutoMapper;
using GuideMountainsMVC.Application.Mapping;
using GuideMountainsMVC.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.ViewModel.MountainPlaceVm
{
    public class NewMountainPlaceVm : IMapFrom<MountainPlace>
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public IFormFile ImageContent { get; set; }  // 🟢 NIE dodawaj [Required] ani [BindRequired]

        public List<SelectListItem> Countries { get; set; } = new List<SelectListItem>();

        public void ConfigureMapping(Profile profile)
        {
            profile.CreateMap<MountainPlace, NewMountainPlaceVm>();
            profile.CreateMap<NewMountainPlaceVm, MountainPlace>()
                .ForMember(dest => dest.Image, opt => opt.Ignore()); // ważne
            profile.CreateMap<MountainPlace, MountainPlaceForListVm>();
        }
    }
}
