using System.ComponentModel.DataAnnotations;

namespace PassengerTransportationAPI.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleID { get; set; }
        public string Photo { get; set; }
        public string Model { get; set; }
        public string RegistrationNumber { get; set; }
        public int Capacity { get; set; }
    }
}
