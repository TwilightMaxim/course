using Microsoft.EntityFrameworkCore;
using PassengerTransportationAPI.Models;

namespace PassengerTransportationAPI
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public DbSet<UserDatas> UserData { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<TransportRoutes> TransportRoute { get; set; }
    }
}
