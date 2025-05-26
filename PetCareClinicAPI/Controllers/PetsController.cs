using Microsoft.AspNetCore.Mvc;
using PetCareClinicAPI.Models.Domain;
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
                    AddressId = 201,
                    Address = new Address
                    {
                        Id = 201,
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
                    AddressId = 202,
                    Address = new Address
                    {
                        Id = 202,
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
                    AddressId = 203,
                    Address = new Address
                    {
                        Id = 203,
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
                    AddressId = 204,
                    Address = new Address
                    {
                        Id = 204,
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
                    AddressId = 205,
                    Address = new Address
                    {
                        Id = 205,
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
                    AddressId = 206,
                    Address = new Address
                    {
                        Id = 206,
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
                    AddressId = 207,
                    Address = new Address
                    {
                        Id = 207,
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
                    AddressId = 208,
                    Address = new Address
                    {
                        Id = 208,
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
                    AddressId = 209,
                    Address = new Address
                    {
                        Id = 209,
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
