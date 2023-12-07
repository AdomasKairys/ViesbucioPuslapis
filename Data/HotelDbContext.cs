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

    }
}
