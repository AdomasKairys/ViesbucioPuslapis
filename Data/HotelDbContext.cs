using Microsoft.EntityFrameworkCore;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Data
{
    public class HotelDbContext: DbContext
    {
        public HotelDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Trainer> treneris { get; set; }
        public DbSet<RoomTypes> kambario_tipas { get; set; }
        public DbSet<TrainingSession> treniruote { get; set; }
        public DbSet<GymReservation> sporto_sales_rezervacija { get; set; }
    }
}
