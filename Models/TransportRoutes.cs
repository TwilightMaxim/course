using System.ComponentModel.DataAnnotations;

namespace PassengerTransportationAPI.Models
{
    public class TransportRoutes
    {
        [Key]
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public string PlaceArrival { get; set; }
        public string PlaceDeparture { get; set; }
        public string CityArrival { get; set; }
        public string CityDeparture { get; set; }
        public int Cost { get; set; }
        public int NumberSeats { get; set; }
        public int VehicleId { get; set; }
    }
}
