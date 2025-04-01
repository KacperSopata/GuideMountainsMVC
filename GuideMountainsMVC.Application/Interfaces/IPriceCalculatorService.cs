using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideMountainsMVC.Application.Interfaces
{
    public interface IPriceCalculatorService
    {
        double CalculateSkiPassPrice(int skiPassId, int skiPassTypeId, int days);
        double CalculateAccommodationPrice(int accommodationId, DateTime start, DateTime end, int guests);
        double CalculateEquipmentPrice(int equipmentRentalId, int days);
    }
}
