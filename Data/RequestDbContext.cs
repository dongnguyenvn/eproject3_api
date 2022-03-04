using Microsoft.EntityFrameworkCore;
using web_api.Model;

namespace web_api.Data
{
    public class RequestDbContext : DbContext
    {
        public RequestDbContext(DbContextOptions<RequestDbContext> options) : base(options)
        {

        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RequestRoom> RequestRooms { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<RequestEquipment> RequestEquipments { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
    }
}