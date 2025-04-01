using GuideMountainsMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetWeatherAsync(string city);
        Task<WeatherFiveDayForecast> GetFiveDayForecastAsync(string city);


    }
}
