﻿using Microsoft.EntityFrameworkCore;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Data
{
    public class HotelDbContext: DbContext
    {
        public HotelDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Trainer> treneris { get; set; }
        public DbSet<TrainingSession> treniruote { get; set; }
        public DbSet<GymReservation> sporto_sales_rezervacija { get; set; }
		
		public DbSet<RoomTypes> kambario_tipas { get; set; }
        public DbSet<Room> kambarys { get; set; }
        public DbSet<Reservation> kambario_rezervacija { get; set; }

        public DbSet<Client> klientas { get; set; }
        public DbSet<Admin> administratorius { get; set; }

        public DbSet<User> naudotojas { get; set; }

        public DbSet<ParkingPlace> stovejimo_vieta { get; set; }
        public DbSet<ParkingReservation> stovejimo_vietos_rezervacija { get; set; }
    }
}
