using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Domain.Model;
using GuideMountainsMVC.Application.Services;
using GuideMountainsMVC.Application.ViewModel.AccommodationVm;

namespace GuideMountainsMVC.Tests
{
    public class AccommodationServiceTests
    {
        [Fact]
        public void GetAccommodationDetail_ReturnsCorrectDetail()
        {
            // Arrange
            var mockRepo = new Mock<IAccommodationRepository>();
            var mockMapper = new Mock<IMapper>();

            var id = 1;
            var accommodation = new Accommodation { Id = id, Name = "Mountain View" };
            var accommodationVm = new AccommodationDetailVm { Id = id, Name = "Mountain View" };

            mockRepo.Setup(r => r.GetAccommodationById(id)).Returns(accommodation);
            mockMapper.Setup(m => m.Map<AccommodationDetailVm>(accommodation)).Returns(accommodationVm);

            var service = new AccommodationService(mockRepo.Object, mockMapper.Object);

            // Act
            var result = service.GetAccommodationDetail(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Mountain View", result.Name);
        }

        [Fact]
        public void AddAccommodation_ValidModel_ReturnsId()
        {
            var mockRepo = new Mock<IAccommodationRepository>();
            var mockMapper = new Mock<IMapper>();

            var vm = new NewAccommodationVm { Name = "New Place" };
            var mapped = new Accommodation { Id = 101 };

            mockMapper.Setup(m => m.Map<Accommodation>(vm)).Returns(mapped);

            var service = new AccommodationService(mockRepo.Object, mockMapper.Object);

            // Act
            var id = service.AddAccommodation(vm);

            // Assert
            mockRepo.Verify(r => r.AddAccommodation(mapped), Times.Once);
            Assert.Equal(101, id);
        }

        [Fact]
        public void DeleteAccommodation_ValidId_DeletesEntity()
        {
            var mockRepo = new Mock<IAccommodationRepository>();
            var mockMapper = new Mock<IMapper>();

            var acc = new Accommodation { Id = 7 };
            mockRepo.Setup(r => r.GetAccommodationById(7)).Returns(acc);

            var service = new AccommodationService(mockRepo.Object, mockMapper.Object);

            // Act
            service.DeleteAccommodation(7);

            // Assert
            mockRepo.Verify(r => r.DeleteAccommodation(acc), Times.Once);
        }

        [Fact]
        public void GetAccommodationsByMountainPlaceId_ReturnsMappedList()
        {
            var mockRepo = new Mock<IAccommodationRepository>();
            var mockMapper = new Mock<IMapper>();

            var accList = new List<Accommodation> { new Accommodation { Id = 1 }, new Accommodation { Id = 2 } };
            var vmList = new List<AccommodationForListVm> { new AccommodationForListVm { Id = 1 }, new AccommodationForListVm { Id = 2 } };

            mockRepo.Setup(r => r.GetAccommodationsByMountainPlaceId(5)).Returns(accList);
            mockMapper.Setup(m => m.Map<List<AccommodationForListVm>>(accList)).Returns(vmList);

            var service = new AccommodationService(mockRepo.Object, mockMapper.Object);

            // Act
            var result = service.GetAccommodationsByMountainPlaceId(5);

            // Assert
            Assert.Equal(2, result.Accommodations.Count);
        }

        [Fact]
        public void GetAllAccommodations_WithSearch_ReturnsFilteredResults()
        {
            var mockRepo = new Mock<IAccommodationRepository>();
            var mockMapper = new Mock<IMapper>();

            var accs = new List<Accommodation>
            {
                new Accommodation { Id = 1, Name = "Alp House" },
                new Accommodation { Id = 2, Name = "Mountain View" },
                new Accommodation { Id = 3, Name = "Beach Villa" }
            }.AsQueryable();

            mockRepo.Setup(r => r.GetAllAccommodations()).Returns(accs);
            mockMapper.Setup(m => m.Map<List<AccommodationForListVm>>(It.IsAny<List<Accommodation>>()))
                      .Returns(new List<AccommodationForListVm> { new AccommodationForListVm { Id = 1 }, new AccommodationForListVm { Id = 2 } });

            var service = new AccommodationService(mockRepo.Object, mockMapper.Object);

            // Act
            var result = service.GetAllAccommodations(10, 1, "Mountain");

            // Assert
            Assert.Equal(2, result.Accommodations.Count);
        }
        [Fact]
        public void UpdateAccommodation_ValidModel_UpdatesEntity()
        {
            // Arrange
            var mockRepo = new Mock<IAccommodationRepository>();
            var mockMapper = new Mock<IMapper>();

            var vm = new NewAccommodationVm { Id = 1, Name = "Updated" };
            var mapped = new Accommodation { Id = 1, Name = "Updated" };

            mockMapper.Setup(m => m.Map<Accommodation>(vm)).Returns(mapped);

            var service = new AccommodationService(mockRepo.Object, mockMapper.Object);

            // Act
            service.UpdateAccommodation(vm);

            // Assert
            mockRepo.Verify(r => r.UpdateAccommodation(mapped), Times.Once);
        }
        [Fact]
        public void GetAccommodationByCountryId_ReturnsMappedList()
        {
            // Arrange
            var mockRepo = new Mock<IAccommodationRepository>();
            var mockMapper = new Mock<IMapper>();

            var accList = new List<Accommodation>
            {
                new Accommodation { Id = 1, CountryId = 10, Description = "Desc 1" },
                new Accommodation { Id = 2, CountryId = 10, Description = "Desc 2" }
            };

            mockRepo.Setup(r => r.GetByCountryId(10)).Returns(accList.AsQueryable());

            var service = new AccommodationService(mockRepo.Object, mockMapper.Object);

            // Act
            var result = service.GetAccommodationByCountryId(10);

            // Assert
            Assert.Equal(2, result.Accommodations.Count);
            Assert.All(result.Accommodations, a => Assert.Equal(10, a.CountryId));
        }
    }
}
