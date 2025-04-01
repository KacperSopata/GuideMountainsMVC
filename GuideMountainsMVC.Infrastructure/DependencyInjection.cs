using GuideMountainsMVC.Application.Services;
using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Infrastructure
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IMountainPlaceRepository, MountainPlaceRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ISkiPassRepository, SkiPassRepository>();
            services.AddTransient<IAccommodationRepository, AccommodationRepository>();
            services.AddTransient<IEquipmentRentalRepository, EquipmentRentalRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<ISkiPassTypeRepository, SkiPassTypeRepository>();
            services.AddTransient<IAccommodationReservationRepository, AccommodationReservationRepository>();
            return services;
        }
    }
}
