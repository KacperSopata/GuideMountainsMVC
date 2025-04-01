using Xunit;
using Moq;
using System.Collections.Generic;
using GuideMountainsMVC.Domain.Interfaces;
using Xunit;
using Moq;
using System.Collections.Generic;
using GuideMountainsMVC.Application.Services;
using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Domain.Model;
using AutoMapper;
using GuideMountainsMVC.Application.ViewModel.EquipmentRentalVm;

namespace GuideMountainsMVC.Test
{
    public class EquipmentRentalServiceTests
    {
        [Fact]
        public void GetAllCategories_ReturnsAllCategoryEquipmentRental()
        {
            // Arrange
            var mockRepo = new Mock<IEquipmentRentalRepository>();
            var mockMapper = new Mock<IMapper>();

            mockRepo.Setup(repo => repo.GetAllCategories()).Returns(new List<CategoryEquipmentRental>
            {
                new CategoryEquipmentRental { Id = 1, Name = "Ski" },
                new CategoryEquipmentRental { Id = 2, Name = "Snowboard" }
            });

            var service = new EquipmentRentalService(mockRepo.Object, mockMapper.Object);

            // Act
            var result = service.GetAllCategories();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Name == "Ski");
            Assert.Contains(result, c => c.Name == "Snowboard");
        }
        [Fact]
        public void GetEquipmentRentalDetail_ReturnsCorrectDetail()
        {
            // Arrange
            var mockRepo = new Mock<IEquipmentRentalRepository>();
            var mockMapper = new Mock<IMapper>();

            int id = 1;
            var rentalEntity = new EquipmentRental { Id = id, Name = "Test Rental" };
            var rentalVm = new EquipmentRentalDetailVm { Id = id, Name = "Test Rental" };

            mockRepo.Setup(repo => repo.GetEquipmentRentalById(id)).Returns(rentalEntity);
            mockMapper.Setup(m => m.Map<EquipmentRentalDetailVm>(rentalEntity)).Returns(rentalVm);

            var service = new EquipmentRentalService(mockRepo.Object, mockMapper.Object);

            // Act
            var result = service.GetEquipmentRentalDetail(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
            Assert.Equal("Test Rental", result.Name);
        }
        [Fact]
        public void AddEquipmentRental_WithValidModel_AddsRentalAndReturnsId()
        {
            // Arrange
            var mockRepo = new Mock<IEquipmentRentalRepository>();
            var mockMapper = new Mock<IMapper>();

            var newRentalVm = new NewEquipmentRentalVm
            {
                Name = "Test Equipment",
                Description = "Test description"
            };

            var equipment = new EquipmentRental { Id = 42 };

            mockMapper.Setup(m => m.Map<EquipmentRental>(It.IsAny<NewEquipmentRentalVm>())).Returns(equipment);

            var service = new EquipmentRentalService(mockRepo.Object, mockMapper.Object);

            // Act
            var resultId = service.AddEquipmentRental(newRentalVm);

            // Assert
            mockRepo.Verify(r => r.AddEquipmentRental(It.Is<EquipmentRental>(e => e == equipment)), Times.Once);
            Assert.Equal(42, resultId);
        }
        [Fact]
        public void DeleteEquipmentRental_ValidId_DeletesRental()
        {
            // Arrange
            var mockRepo = new Mock<IEquipmentRentalRepository>();
            var mockMapper = new Mock<IMapper>();

            var equipment = new EquipmentRental { Id = 10 };

            mockRepo.Setup(r => r.GetEquipmentRentalById(10)).Returns(equipment);

            var service = new EquipmentRentalService(mockRepo.Object, mockMapper.Object);

            // Act
            service.DeleteEquipmentRental(10);

            // Assert
            mockRepo.Verify(r => r.DeleteEquipmentRental(equipment), Times.Once);
        }
        [Fact]
        public void GetAllEquipmentRentals_WithSearch_ReturnsPagedAndFilteredResults()
        {
            // Arrange
            var mockRepo = new Mock<IEquipmentRentalRepository>();
            var mockMapper = new Mock<IMapper>();

            var equipmentList = new List<EquipmentRental>
            {
        new EquipmentRental { Id = 1, Name = "Ski", Description = "Downhill" },
        new EquipmentRental { Id = 2, Name = "Snowboard", Description = "Freestyle" },
        new EquipmentRental { Id = 3, Name = "Skis", Description = "Classic" }
            }.AsQueryable();

            mockRepo.Setup(r => r.GetAllEquipmentRentals()).Returns(equipmentList);

            mockMapper.Setup(m => m.Map<List<EquipmentRentalForListVm>>(It.IsAny<List<EquipmentRental>>()))
                      .Returns(new List<EquipmentRentalForListVm>
                      {
                  new EquipmentRentalForListVm { Id = 1, Name = "Ski" },
                  new EquipmentRentalForListVm { Id = 3, Name = "Skis" }
                      });

            var service = new EquipmentRentalService(mockRepo.Object, mockMapper.Object);

            // Act
            var result = service.GetAllEquipmentRentals(10, 1, "Ski");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.EquipmentRentals.Count); // dwa pasuj¹ce po filtrze
            Assert.Equal(2, result.Count); // te¿ 2 bo tylko 2 pasuj¹
            mockRepo.Verify(r => r.GetAllEquipmentRentals(), Times.Once);
        }


    }
}
