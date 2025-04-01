using Xunit;
using Moq;
using AutoMapper;
using System;
using System.Collections.Generic;
using GuideMountainsMVC.Application.Services;
using GuideMountainsMVC.Application.ViewModel.ReservationVm;
using GuideMountainsMVC.Domain.Interfaces;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Tests
{
    public class ReservationServiceTests
    {
        private readonly Mock<IReservationRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ReservationService _service;

        public ReservationServiceTests()
        {
            _mockRepo = new Mock<IReservationRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new ReservationService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public void AddItemToReservation_AddsCorrectItem()
        {
            var itemVm = new ReservationItemVm { SkiPassId = 1, Price = 100 };

            _service.AddItemToReservation(5, itemVm);

            _mockRepo.Verify(r => r.AddItemToReservation(5, It.IsAny<ReservationItem>()), Times.Once);
        }

        [Fact]
        public void ConfirmReservation_ValidId_UpdatesReservation()
        {
            var reservation = new Reservation { Id = 10, IsConfirmed = false };
            _mockRepo.Setup(r => r.GetReservationById(10)).Returns(reservation);

            _service.ConfirmReservation(10);

            Assert.True(reservation.IsConfirmed);
            _mockRepo.Verify(r => r.UpdateReservation(reservation), Times.Once);
        }

        [Fact]
        public void CreateReservation_ValidInput_ReturnsId()
        {
            var vm = new NewReservationVm { MountainPlaceId = 2, UserId = "user123" };
            _mockRepo.Setup(r => r.GetMountainPlaceById(2)).Returns(new MountainPlace { Id = 2, Name = "Tatry" });

            int returnedId = 0;
            _mockRepo.Setup(r => r.AddReservation(It.IsAny<Reservation>()))
                .Callback<Reservation>(r => returnedId = r.Id = 99);

            var result = _service.CreateReservation(vm);

            Assert.Equal(99, result);
        }

        [Fact]
        public void GetReservationDetails_ReturnsMappedVm()
        {
            var entity = new Reservation { Id = 1 };
            var vm = new ReservationVm { Id = 1 };

            _mockRepo.Setup(r => r.GetReservationById(1)).Returns(entity);
            _mockMapper.Setup(m => m.Map<ReservationVm>(entity)).Returns(vm);

            var result = _service.GetReservationDetails(1);

            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void GetUserReservations_ReturnsMappedList()
        {
            var list = new List<Reservation> { new Reservation { Id = 1 }, new Reservation { Id = 2 } };
            var vmList = new List<ReservationVm> { new ReservationVm { Id = 1 }, new ReservationVm { Id = 2 } };

            _mockRepo.Setup(r => r.GetUserReservations()).Returns(list);
            _mockMapper.Setup(m => m.Map<List<ReservationVm>>(list)).Returns(vmList);

            var result = _service.GetUserReservations();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void RemoveItemFromReservation_RemovesItem()
        {
            _service.RemoveItemFromReservation(1, 99);

            _mockRepo.Verify(r => r.RemoveItem(1, 99), Times.Once);
        }

        [Fact]
        public void DeleteReservation_ValidId_DeletesReservation()
        {
            var reservation = new Reservation { Id = 3 };
            _mockRepo.Setup(r => r.GetReservationById(3)).Returns(reservation);

            _service.DeleteReservation(3);

            _mockRepo.Verify(r => r.DeleteReservation(reservation), Times.Once);
        }

        [Fact]
        public void DeleteReservation_InvalidId_ThrowsException()
        {
            _mockRepo.Setup(r => r.GetReservationById(5)).Returns<Reservation>(null);

            Assert.Throws<Exception>(() => _service.DeleteReservation(5));
        }
    }
}
