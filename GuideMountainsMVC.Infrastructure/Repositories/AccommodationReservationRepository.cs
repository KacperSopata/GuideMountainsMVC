//using GuideMountainsMVC.Domain.Interfaces;
//using GuideMountainsMVC.Domain.Model;
//using GuideMountainsMVC.Infrastructure;

//public class AccommodationReservationRepository : IAccommodationReservationRepository
//{
//    private readonly GuideMountainsMVCDbContext _context;

//    public AccommodationReservationRepository(GuideMountainsMVCDbContext context)
//    {
//        _context = context;
//    }

//    public void AddReservation(AccommodationReservation reservation)
//    {
//        _context.AccommodationReservations.Add(reservation);
//        _context.SaveChanges();
//    }

//    public bool IsAccommodationAvailable(int accommodationId, DateTime start, DateTime end)
//    {
//        return !_context.AccommodationReservations.Any(r =>
//            r.AccommodationId == accommodationId &&
//            ((start >= r.StartDate && start < r.EndDate) ||
//             (end > r.StartDate && end <= r.EndDate) ||
//             (start <= r.StartDate && end >= r.EndDate))
//        );
//    }

//    public List<AccommodationReservation> GetReservationsForAccommodation(int accommodationId)
//    {
//        return _context.AccommodationReservations
//                       .Where(r => r.AccommodationId == accommodationId)
//                       .ToList();
//    }
//}
