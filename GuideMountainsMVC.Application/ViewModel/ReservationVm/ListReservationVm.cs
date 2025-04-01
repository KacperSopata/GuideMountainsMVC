using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuideMountainsMVC.Application.ViewModel.AccommodationVm;
using GuideMountainsMVC.Domain.Model;

namespace GuideMountainsMVC.Application.ViewModel.ReservationVm
{
    class ListReservationVm
    {
        public List<ReservationItemVm> Reservations { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
    }
}
