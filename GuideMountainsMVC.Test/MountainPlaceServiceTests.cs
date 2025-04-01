using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using GuideMountainsMVC.Domain.Model;
using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Application.Services;
using GuideMountainsMVC.Application.ViewModel.MountainPlaceVm;
using GuideMountainsMVC.Application.ViewModel.CountryVm;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace GuideMountainsMVC.Tests
{
    public class MountainPlaceServiceTests
    {
        private readonly Mock<IMountainPlaceRepository> _mockPlaceRepo;
        private readonly Mock<ICountryRepository> _mockCountryRepo;
        private readonly Mock<ISkiPassRepository> _mockSkiRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly MountainPlaceService _service;

        public MountainPlaceServiceTests()
        {
            _mockPlaceRepo = new Mock<IMountainPlaceRepository>();
            _mockCountryRepo = new Mock<ICountryRepository>();
            _mockSkiRepo = new Mock<ISkiPassRepository>();
            _mockMapper = new Mock<IMapper>();

            _service = new MountainPlaceService(
                _mockPlaceRepo.Object,
                _mockCountryRepo.Object,
                _mockSkiRepo.Object,
                _mockMapper.Object);
        }

        [Fact]
        public void GetMountainPlaceDetails_ReturnsCorrectVm()
        {
            var place = new MountainPlace { Id = 1, Name = "Alps", Description = "Nice", CountryId = 1 };
            _mockPlaceRepo.Setup(r => r.GetById(1)).Returns(place);

            var result = _service.GetMountainPlaceDetails(1);

            Assert.NotNull(result);
            Assert.Equal("Alps", result.Name);
        }

        [Fact]
        public void AddMountainPlace_WithImage_SavesAndReturnsId()
        {
            // Arrange
            var imageBytes = Encoding.UTF8.GetBytes("fake image content");
            var stream = new MemoryStream(imageBytes);
            var formFile = new FormFile(stream, 0, stream.Length, "ImageContent", "test.jpg")
            {
                Headers = new HeaderDictionary(),
                ContentType = "image/jpeg"
            };

            var vm = new NewMountainPlaceVm
            {
                Name = "Test",
                ImageContent = formFile,
                Image = imageBytes
            };

            var mapped = new MountainPlace { Id = 12 };

            _mockMapper.Setup(m => m.Map<MountainPlace>(vm)).Returns(mapped);
            _mockPlaceRepo.Setup(r => r.Add(mapped)).Returns(12);

            // Act
            var result = _service.AddMountainPlace(vm);

            // Assert
            Assert.Equal(12, result);
        }

        [Fact]
        public void DeleteMountainPlace_ValidId_DeletesEntity()
        {
            var place = new MountainPlace { Id = 7 };
            _mockPlaceRepo.Setup(r => r.GetById(7)).Returns(place);

            _service.DeleteMountainPlace(7);

            _mockPlaceRepo.Verify(r => r.Delete(7), Times.Once);
        }

        [Fact]
        public void GetAllMountainPlacesForList_ReturnsFilteredPagedData()
        {
            // Arrange
            var mockRepo = new Mock<IMountainPlaceRepository>();
            var mockCountryRepo = new Mock<ICountryRepository>();
            var mockSkiPassRepo = new Mock<ISkiPassRepository>();
            var mockMapper = new Mock<IMapper>();

            var places = new List<MountainPlace>
            {
                new MountainPlace { Id = 1, Name = "Alp Resort", Description = "Alpine beauty", CountryId = 1 },
                new MountainPlace { Id = 2, Name = "Alpine Base", Description = "Base camp", CountryId = 1 },
                new MountainPlace { Id = 3, Name = "Snowhill", Description = "Snowy", CountryId = 2 }
            }.AsQueryable();

            mockRepo.Setup(r => r.GetAll()).Returns(places);

            var service = new MountainPlaceService(mockRepo.Object, mockCountryRepo.Object, mockSkiPassRepo.Object, mockMapper.Object);

            // Act
            var result = service.GetAllMountainPlacesForList(10, 1, "Alp"); // Szukamy "Alp"

            // Assert
            Assert.Equal(2, result.MountainPlaces.Count); // Powinny być 2: Alp Resort i Alpine Base
            Assert.Equal(2, result.Count); // Całkowita liczba dopasowań
        }


        [Fact]
        public void GetAllMountainPlacesForCountry_ReturnsCorrectList()
        {
            var list = new List<MountainPlace> { new MountainPlace { Id = 1, CountryId = 5 }, new MountainPlace { Id = 2, CountryId = 5 } };
            _mockPlaceRepo.Setup(r => r.GetByCountryId(5)).Returns(list);

            var result = _service.GetAllMountainPlacesForCountry(5);

            Assert.Equal(2, result.MountainPlaces.Count);
        }

        [Fact]
        public void GetMountainPlacesByCountryId_ReturnsMappedVm()
        {
            var list = new List<MountainPlace> { new MountainPlace { Id = 1, CountryId = 3 } };
            _mockPlaceRepo.Setup(r => r.GetByCountryId(3)).Returns(list);

            var result = _service.GetMountainPlacesByCountryId(3);

            Assert.Single(result.MountainPlaces);
        }

        [Fact]
        public void GetAllCountries_ReturnsMappedList()
        {
            var countryList = new List<Country> { new Country { Id = 1, Name = "Italy" } };
            var countryVmList = new List<CountryForListVm> { new CountryForListVm { Id = 1, Name = "Italy" } };

            _mockCountryRepo.Setup(r => r.GetAllCountries()).Returns(countryList);
            _mockMapper.Setup(m => m.Map<List<CountryForListVm>>(countryList)).Returns(countryVmList);

            var result = _service.GetAllCountries();

            Assert.Single(result);
        }

        [Fact]
        public void UpdateMountainPlace_ValidModel_UpdatesEntity()
        {
            var existing = new MountainPlace { Id = 99 };
            var vm = new NewMountainPlaceVm { Id = 99, Name = "Updated", Description = "New", CountryId = 2, Image = new byte[] { 1, 2 } };

            _mockPlaceRepo.Setup(r => r.GetById(99)).Returns(existing);

            _service.UpdateMountainPlace(vm);

            _mockPlaceRepo.Verify(r => r.Update(It.Is<MountainPlace>(p =>
                p.Name == "Updated" &&
                p.Description == "New" &&
                p.CountryId == 2 &&
                p.Image.SequenceEqual(vm.Image)
            )), Times.Once);
        }
    }
}
