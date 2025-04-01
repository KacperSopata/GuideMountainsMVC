using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using GuideMountainsMVC.Application.Services;
using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Tests
{
    public class SkiPassTypeServiceTests
    {
        private readonly Mock<ISkiPassTypeRepository> _mockRepo;
        private readonly SkiPassTypeService _service;

        public SkiPassTypeServiceTests()
        {
            _mockRepo = new Mock<ISkiPassTypeRepository>();
            _service = new SkiPassTypeService(_mockRepo.Object);
        }
        [Fact]
        public void GetAll_ReturnsAllTypes()
        {
            // Arrange
            var types = new List<SkiPassType>
            {
                new SkiPassType { Id = 1, Name = "Regular" },
                new SkiPassType { Id = 2, Name = "Discounted" }
            }.AsQueryable();

            _mockRepo.Setup(r => r.GetAll()).Returns(types);

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, t => t.Name == "Regular");
        }
        [Fact]
        public void GetSkiPassTypeById_ValidId_ReturnsCorrectType()
        {
            var type = new SkiPassType { Id = 1, Name = "Student" };
            _mockRepo.Setup(r => r.GetById(1)).Returns(type);

            var result = _service.GetSkiPassTypeById(1);

            Assert.NotNull(result);
            Assert.Equal("Student", result.Name);
        }

        [Fact]
        public void GetSkiPassTypeById_InvalidId_ReturnsNull()
        {
            _mockRepo.Setup(r => r.GetById(99)).Returns((SkiPassType)null);

            var result = _service.GetSkiPassTypeById(99);

            Assert.Null(result);
        }
    }
}
