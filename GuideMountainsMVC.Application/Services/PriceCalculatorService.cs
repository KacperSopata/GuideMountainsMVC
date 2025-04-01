using System;
using GuideMountainsMVC.Application.Interfaces;

namespace GuideMountainsMVC.Application.Services
{
    public class PriceCalculatorService : IPriceCalculatorService
    {
        private readonly ISkiPassTypeService _skiPassTypeService;
        private readonly ISkiPassService _skiPassService;
        private readonly IAccommodationService _accommodationService;
        private readonly IEquipmentRentalService _equipmentService;

        public PriceCalculatorService(
            ISkiPassTypeService skiPassTypeService,
            ISkiPassService skiPassService,
            IAccommodationService accommodationService,
            IEquipmentRentalService equipmentService)
        {
            _skiPassTypeService = skiPassTypeService;
            _skiPassService = skiPassService;
            _accommodationService = accommodationService;
            _equipmentService = equipmentService;
        }

        public double CalculateSkiPassPrice(int skiPassId, int skiPassTypeId, int days)
        {
            var skiPass = _skiPassService.GetSkiPassById(skiPassId);
            var type = _skiPassTypeService.GetSkiPassTypeById(skiPassTypeId);

            if (skiPass == null || type == null || days <= 0)
                return 0;

            return skiPass.BasePrice * type.PriceMultiplier * days;
        }

        public double CalculateAccommodationPrice(int accommodationId, DateTime start, DateTime end, int guests)
        {
            var accommodation = _accommodationService.GetAccommodationDetail(accommodationId);

            int nights = (end - start).Days;
            if (nights < 1) nights = 1;
            if (guests < 1) guests = 1;

            return accommodation.PricePerNight * nights * guests;
        }

        public double CalculateEquipmentPrice(int equipmentRentalId, int days)
        {
            var equipment = _equipmentService.GetEquipmentRentalById(equipmentRentalId);

            if (equipment == null || days <= 0)
                return 0;

            return equipment.PricePerDay * days;
        }


    }
}
