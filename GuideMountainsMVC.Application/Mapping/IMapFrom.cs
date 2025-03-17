using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.Mapping
{
    public interface IMapFrom<T>
    {
        void ConfigureMapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}