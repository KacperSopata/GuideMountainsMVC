using Xunit;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Domain.Model;
using GuideMountainsMVC.Application.Services;
using GuideMountainsMVC.Application.ViewModel.SkiPassVm;

namespace GuideMountainsMVC.Tests
{
    public class SkiPassServiceTests
    {
        private readonly Mock<ISkiPassRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ISkiPassTypeRepository> _mockTypeRepo;
        private readonly SkiPassService _service;

        public SkiPassServiceTests()
        {
            _mockRepo = new Mock<ISkiPassRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockTypeRepo = new Mock<ISkiPassTypeRepository>();
            _service = new SkiPassService(_mockRepo.Object, _mockMapper.Object, _mockTypeRepo.Object);
        }

        [Fact]
        public void GetSkiPassDetail_ReturnsCorrectVm()
        {
            var ski = new SkiPass { Id = 1 };
            var vm = new SkiPassDetailVm { Id = 1 };

            _mockRepo.Setup(r => r.GetSkiPassById(1)).Returns(ski);
            _mockMapper.Setup(m => m.Map<SkiPassDetailVm>(ski)).Returns(vm);

            var result = _service.GetSkiPassDetail(1);

            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void AddSkiPass_WithImage_SavesAndReturnsId()
        {
            var vm = new NewSkiPassVm { Description = "Test description" }; // ✅
            var ski = new SkiPass { Id = 5 };

            _mockMapper.Setup(m => m.Map<SkiPass>(vm)).Returns(ski);

            var result = _service.AddSkiPass(vm);

            _mockRepo.Verify(r => r.AddSkiPass(ski), Times.Once);
            Assert.Equal(5, result);
        }

        [Fact]
        public void DeleteSkiPass_ExistingId_CallsDelete()
        {
            var ski = new SkiPass { Id = 7 };
            _mockRepo.Setup(r => r.GetSkiPassById(7)).Returns(ski);

            _service.DeleteSkiPass(7);

            _mockRepo.Verify(r => r.DeleteSkiPass(ski), Times.Once);
        }

        [Fact]
        public void GetSkiPassByCountryId_ReturnsMappedList()
        {
            var skiList = new List<SkiPass> { new SkiPass { Id = 1, CountryId = 2 } };
            _mockRepo.Setup(r => r.GetByCountryId(2)).Returns(skiList.AsQueryable());

            var result = _service.GetSkiPassByCountryId(2);

            Assert.Single(result.SkiPasses);
            Assert.Equal(1, result.SkiPasses.First().Id);
        }

        [Fact]
        public void GetAllSkiPassTypes_ReturnsAll()
        {
            _mockRepo.Setup(r => r.GetAllSkiPassTypes()).Returns(new List<SkiPassType>
            {           
                new SkiPassType { Id = 1, Name = "Normal" },
                new SkiPassType { Id = 2, Name = "Discounted" }
            }.AsQueryable());

            var result = _service.GetAllSkiPassTypes();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetSkiPassTypeById_ReturnsMappedType()
        {
            var entity = new SkiPassType { Id = 1, Name = "Senior" };
            var vm = new SkiPassTypeVm { Id = 1, Name = "Senior" };

            _mockTypeRepo.Setup(r => r.GetById(1)).Returns(entity);
            _mockMapper.Setup(m => m.Map<SkiPassTypeVm>(entity)).Returns(vm);

            var result = _service.GetSkiPassTypeById(1);

            Assert.Equal("Senior", result.Name);
        }

        [Fact]
        public void GetSkiPassById_ReturnsMappedVm()
        {
            var entity = new SkiPass { Id = 9 };
            var vm = new SkiPassVm { Id = 9 };

            _mockRepo.Setup(r => r.GetSkiPassById(9)).Returns(entity);
            _mockMapper.Setup(m => m.Map<SkiPassVm>(entity)).Returns(vm);

            var result = _service.GetSkiPassById(9);

            Assert.Equal(9, result.Id);
        }
    }
}
