using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuideMountainsMVC.Application.ViewModel.ReservationVm;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Application.Interfaces
{
    public interface IReservationService
    {
        int CreateReservation(NewReservationVm reservationVm);
        void AddItemToReservation(int reservationId, ReservationItemVm item);
        void RemoveItemFromReservation(int reservationId, int itemId);
        List<ReservationVm> GetUserReservations();
        ReservationVm GetReservationDetails(int id);
        void ConfirmReservation(int reservationId);
        List<ReservationForListVm> GetReservationsForUser(string userId);
        void DeleteReservation(int id);
    }
}
