using System.ComponentModel.DataAnnotations;

namespace PassengerTransportationAPI.Models
{
    public class UserDatas
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
    }
}
