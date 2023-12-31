﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using ViesbucioPuslapis.Data;
using ViesbucioPuslapis.Models;

namespace ViesbucioPuslapis.Pages.ClientSystem
{
    public class ClientDetailsModel : PageModel
    {
        private readonly ILogger<ErrorModel> _logger;
        private readonly HotelDbContext _db;
        public List<Client> Clients { get; set; } = new List<Client>();
        public List<User> Users { get; set; } = new List<User>();

        public Client Client { get; set; } // Pridėkite šią eilutę
        public User User { get; set; } // Pridėkite šią eilutę


        public ClientDetailsModel(ILogger<ErrorModel> logger, HotelDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public void OnGet(int id)
        {
            Client = _db.klientas.FirstOrDefault(c => c.id_Naudotojas == id);
            User = _db.naudotojas.FirstOrDefault(c => c.id_Naudotojas == id);
        }
        //public Client Client { get; set; }

        //public void OnGet(int id)
        //{
        //    // Čia galite įdėti kodą gauti kliento duomenis pagal ID iš duomenų bazės
        //    // Šiame pavyzdyje grąžinama kietoji duomenų struktūra
        //    Client = GetClientById(id);
        //}

        //private Client GetClientById(int id)
        //{
        //    // Čia galite įdėti kodą gauti kliento duomenis pagal ID iš duomenų bazės
        //    // Šiame pavyzdyje grąžinama kietoji duomenų struktūra
        //    return new Client { Id = id, Name = "Jonas", Surname = "Jonaitis" };
        //}
    }

}
