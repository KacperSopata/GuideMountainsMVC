using GuideMountainsMVC.Application.Interfaces;
using GuideMountainsMVC.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAplication(this IServiceCollection services)
        {
            services.AddTransient<ISkiPassService, SkiPassService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IMountainPlaceService, MountainPlaceService>();
            services.AddTransient<IAccommodationService, AccommodationService>();
            //services.AddTransient<IChatBotService, ChatBotService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
