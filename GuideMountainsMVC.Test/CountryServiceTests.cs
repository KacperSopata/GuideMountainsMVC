using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using GuideMountainsMVC.Application.Services;
using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Domain.Model;
using GuideMountainsMVC.Application.ViewModel.CountryVm;

namespace GuideMountainsMVC.Tests
{
    public class CountryServiceTests
    {
        private readonly Mock<ICountryRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CountryService _service;

        public CountryServiceTests()
        {
            _mockRepo = new Mock<ICountryRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new CountryService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void GetAllCountries_ReturnsList()
        {
            _mockRepo.Setup(r => r.GetAllCountries()).Returns(new List<Country>
            {
                new Country { Id = 1, Name = "Italy" },
                new Country { Id = 2, Name = "Austria" }
            });

            var result = _service.GetAllCountries();

            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Name == "Italy");
        }

        [Fact]
        public void GetCountryDetail_ValidId_ReturnsViewModel()
        {
            var country = new Country { Id = 1, Name = "Switzerland" };
            var countryVm = new CountryDetailVm { Id = 1, Name = "Switzerland" };

            _mockRepo.Setup(r => r.GetCountryById(1)).Returns(country);
            _mockMapper.Setup(m => m.Map<CountryDetailVm>(country)).Returns(countryVm);

            var result = _service.GetCountryDetail(1);

            Assert.NotNull(result);
            Assert.Equal("Switzerland", result.Name);
        }

        [Fact]
        public void AddCountry_ValidModel_CallsRepository()
        {
            var newCountryVm = new NewCountryVm { Name = "France" };
            var country = new Country { Name = "France" };

            _mockMapper.Setup(m => m.Map<Country>(newCountryVm)).Returns(country);

            _service.AddCountry(newCountryVm);

            _mockRepo.Verify(r => r.AddCountry(It.Is<Country>(c => c.Name == "France")), Times.Once);
        }

        [Fact]
        public void DeleteCountry_ValidId_CallsRepository()
        {
            var country = new Country { Id = 5, Name = "Germany" };
            _mockRepo.Setup(r => r.GetCountryById(5)).Returns(country);

            _service.DeleteCountry(5);

            _mockRepo.Verify(r => r.DeleteCountry(country), Times.Once);
        }

        [Fact]
        public void GetCountriesForDropdown_ReturnsSelectListItems()
        {
            _mockRepo.Setup(r => r.GetAllCountries()).Returns(new List<Country>
            {
                new Country { Id = 1, Name = "Italy" },
                new Country { Id = 2, Name = "Austria" }
            });

            var result = _service.GetCountriesForDropdown().ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal("Italy", result[0].Text);
            Assert.Equal("1", result[0].Value);
        }
    }
}
