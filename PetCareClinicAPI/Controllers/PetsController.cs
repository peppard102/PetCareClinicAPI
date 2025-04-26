using Microsoft.AspNetCore.Mvc;
using PetCareClinicAPI.Data.Models;
using System.Collections.Generic;

namespace PetCareClinicAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ILogger<PetsController> _logger;

        public PetsController(ILogger<PetsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Pet> Get()
        {
            return new List<Pet>
            {
                new Pet
                {
                    Id = 1,
                    FirstName = "Sophie",
                    LastName = "Snow",
                    Age = 15,
                    Address = new Address
                    {
                        Street = "123 Pine Lane",
                        City = "Frostville",
                        State = "WI",
                        ZipCode = "54321"
                    }
                },
                new Pet
                {
                    Id = 2,
                    FirstName = "Chanel",
                    LastName = "Lannister",
                    Age = 6,
                    Address = new Address
                    {
                        Street = "456 Golden Road",
                        City = "Westport",
                        State = "CA",
                        ZipCode = "90210"
                    }
                },
                new Pet
                {
                    Id = 3,
                    FirstName = "Nico",
                    LastName = "Lannister",
                    Age = 3,
                    Address = new Address
                    {
                        Street = "456 Golden Road",
                        City = "Westport",
                        State = "CA",
                        ZipCode = "90210"
                    }
                },
                new Pet
                {
                    Id = 4,
                    FirstName = "Tilly",
                    LastName = "Stark",
                    Age = 6,
                    Address = new Address
                    {
                        Street = "789 Winter Avenue",
                        City = "Northfield",
                        State = "MN",
                        ZipCode = "55057"
                    }
                },
                new Pet
                {
                    Id = 5,
                    FirstName = "Cricket",
                    LastName = "Targaryen",
                    Age = 9,
                    Address = new Address
                    {
                        Street = "321 Dragon Street",
                        City = "Firestone",
                        State = "NV",
                        ZipCode = "89123"
                    }
                },
                new Pet
                {
                    Id = 6,
                    FirstName = "Chester",
                    LastName = "Melisandre",
                    Age = 12,
                    Address = new Address
                    {
                        Street = "654 Ruby Lane",
                        City = "Shadowvale",
                        State = "OR",
                        ZipCode = "97401"
                    }
                },
                new Pet
                {
                    Id = 7,
                    FirstName = "Loki",
                    LastName = "Clifford",
                    Age = 8,
                    Address = new Address
                    {
                        Street = "987 Mischief Road",
                        City = "Tricksburg",
                        State = "NY",
                        ZipCode = "12345"
                    }
                },
                new Pet
                {
                    Id = 8,
                    FirstName = "Kindle",
                    LastName = "Frances",
                    Age = 5,
                    Address = new Address
                    {
                        Street = "135 Reading Way",
                        City = "Bookville",
                        State = "MA",
                        ZipCode = "02138"
                    }
                },
                new Pet
                {
                    Id = 9,
                    FirstName = "Lou",
                    LastName = "Roxie",
                    Age = 2,
                    Address = new Address
                    {
                        Street = "246 Puppy Lane",
                        City = "Playful",
                        State = "TX",
                        ZipCode = "75001"
                    }
                }
            };
        }
    }
}
