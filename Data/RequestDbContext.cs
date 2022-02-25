using Microsoft.EntityFrameworkCore;
using web_api.Model;

namespace web_api.Data
{
    public class RequestDbContext : DbContext
    {
        public RequestDbContext(DbContextOptions<RequestDbContext> options) : base(options)
        {

        }
        public DbSet<RequestRoom> RequestRooms { get; set; }
        public DbSet<Room> Rooms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                .HasOne(r => r.RequestRoom)
                .WithOne(r => r.Room)
                .HasForeignKey<RequestRoom>(r => r.RoomId);
        }
    }
}