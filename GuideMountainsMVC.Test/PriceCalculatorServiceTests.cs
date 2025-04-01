using Xunit;
using Moq;
using System;
using GuideMountainsMVC.Application.Services;
using GuideMountainsMVC.Application.Interfaces;
using GuideMountainsMVC.Application.ViewModel.SkiPassVm;
using GuideMountainsMVC.Application.ViewModel.AccommodationVm;
using GuideMountainsMVC.Application.ViewModel.EquipmentRentalVm;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Tests
{
    public class PriceCalculatorServiceTests
    {
        private readonly Mock<ISkiPassService> _mockSkiService;
        private readonly Mock<ISkiPassTypeService> _mockTypeService;
        private readonly Mock<IAccommodationService> _mockAccService;
        private readonly Mock<IEquipmentRentalService> _mockEquipService;

        private readonly PriceCalculatorService _service;

        public PriceCalculatorServiceTests()
        {
            _mockSkiService = new Mock<ISkiPassService>();
            _mockTypeService = new Mock<ISkiPassTypeService>();
            _mockAccService = new Mock<IAccommodationService>();
            _mockEquipService = new Mock<IEquipmentRentalService>();

                _service = new PriceCalculatorService(
                _mockTypeService.Object,
                _mockSkiService.Object,
                _mockAccService.Object,
                _mockEquipService.Object);
        }

        [Fact]
        public void CalculateSkiPassPrice_ValidInput_ReturnsCorrectPrice()
        {
            _mockSkiService.Setup(s => s.GetSkiPassById(1))
                .Returns(new SkiPassVm { BasePrice = 100 });

            _mockTypeService.Setup(t => t.GetSkiPassTypeById(1)) // ← tutaj poprawiona nazwa
                .Returns(new SkiPassType { PriceMultiplier = 1.5 });

            var result = _service.CalculateSkiPassPrice(1, 1, 3);

            Assert.Equal(450, result);
        }


        [Fact]
        public void CalculateSkiPassPrice_InvalidData_ReturnsZero()
        {
            _mockSkiService.Setup(s => s.GetSkiPassById(1)).Returns((SkiPassVm)null);

            var result = _service.CalculateSkiPassPrice(1, 1, 3);

            Assert.Equal(0, result);
        }

        [Fact]
        public void CalculateAccommodationPrice_ValidData_ReturnsCorrectPrice()
        {
            var start = new DateTime(2024, 1, 1);
            var end = new DateTime(2024, 1, 4); // 3 nights

            _mockAccService.Setup(a => a.GetAccommodationDetail(5)).Returns(new AccommodationDetailVm
            {
                PricePerNight = 200
            });

            var result = _service.CalculateAccommodationPrice(5, start, end, 2);

            Assert.Equal(1200, result); // 3 nights * 200 * 2 guests
        }

        [Fact]
        public void CalculateAccommodationPrice_ZeroNightsOrGuests_ReturnsMinPrice()
        {
            var start = new DateTime(2024, 1, 1);
            var end = new DateTime(2024, 1, 1); // 0 nights

            _mockAccService.Setup(a => a.GetAccommodationDetail(5)).Returns(new AccommodationDetailVm
            {
                PricePerNight = 150
            });

            var result = _service.CalculateAccommodationPrice(5, start, end, 0);

            Assert.Equal(150, result); // min 1 night, min 1 guest
        }

        [Fact]
        public void CalculateEquipmentPrice_Valid_ReturnsCorrect()
        {
            _mockEquipService.Setup(e => e.GetEquipmentRentalById(3)).Returns(new EquipmentRentalBasicVm
            {
                PricePerDay = 50
            });

            var result = _service.CalculateEquipmentPrice(3, 4);

            Assert.Equal(200, result);
        }

        [Fact]
        public void CalculateEquipmentPrice_NullEquipment_ReturnsZero()
        {
            _mockEquipService.Setup(e => e.GetEquipmentRentalById(99)).Returns((EquipmentRentalBasicVm)null);

            var result = _service.CalculateEquipmentPrice(99, 5);

            Assert.Equal(0, result);
        }
    }
}
