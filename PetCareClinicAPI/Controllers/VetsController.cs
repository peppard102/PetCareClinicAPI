using Microsoft.AspNetCore.Mvc;
using PetCareClinicAPI.Models.Domain;

namespace PetCareClinicAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VetsController : ControllerBase
    {
        private readonly ILogger<VetsController> _logger;

        public VetsController(ILogger<VetsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Vet> Get()
        {
            return new List<Vet>
            {
                new Vet
                {
                    Id = 1,
                    FirstName = "Jon",
                    LastName = "Snow",
                    Specialty = "General Practice",
                    AddressId = 101,
                    Address = new Address
                    {
                        Id = 101,
                        Street = "123 Veterinary Lane",
                        City = "Winterfell",
                        State = "NY",
                        ZipCode = "10001"
                    }
                },
                new Vet
                {
                    Id = 2,
                    FirstName = "Cersei",
                    LastName = "Lannister",
                    Specialty = "Surgery",
                    AddressId = 102,
                    Address = new Address
                    {
                        Id = 102,
                        Street = "456 Golden Ave",
                        City = "King's Landing",
                        State = "CA",
                        ZipCode = "90210"
                    }
                },
                new Vet
                {
                    Id = 3,
                    FirstName = "Jaime",
                    LastName = "Lannister",
                    Specialty = "Orthopedic Surgery",
                    AddressId = 103,
                    Address = new Address
                    {
                        Id = 103,
                        Street = "789 Knight Road",
                        City = "Casterly Rock",
                        State = "CA",
                        ZipCode = "90211"
                    }
                },
                new Vet
                {
                    Id = 4,
                    FirstName = "Arya",
                    LastName = "Stark",
                    Specialty = "Emergency Medicine",
                    AddressId = 104,
                    Address = new Address
                    {
                        Id = 104,
                        Street = "321 Faceless Street",
                        City = "Braavos",
                        State = "FL",
                        ZipCode = "33101"
                    }
                },
                new Vet
                {
                    Id = 5,
                    FirstName = "Daenerys",
                    LastName = "Targaryen",
                    Specialty = "Exotic Animals",
                    AddressId = 105,
                    Address = new Address
                    {
                        Id = 105,
                        Street = "654 Dragon Boulevard",
                        City = "Dragonstone",
                        State = "NV",
                        ZipCode = "89123"
                    }
                },
                new Vet
                {
                    Id = 6,
                    FirstName = "Samantha",
                    LastName = "Melisandre",
                    Specialty = "Internal Medicine",
                    AddressId = 106,
                    Address = new Address
                    {
                        Id = 106,
                        Street = "987 Red Temple Way",
                        City = "Asshai",
                        State = "OR",
                        ZipCode = "97401"
                    }
                },
                new Vet
                {
                    Id = 7,
                    FirstName = "Ferrara",
                    LastName = "Clifford",
                    Specialty = "Cardiology",
                    AddressId = 107,
                    Address = new Address
                    {
                        Id = 107,
                        Street = "135 Heart Lane",
                        City = "Oldtown",
                        State = "TX",
                        ZipCode = "75001"
                    }
                },
                new Vet
                {
                    Id = 8,
                    FirstName = "Rossini",
                    LastName = "Frances",
                    Specialty = "Dermatology",
                    AddressId = 108,
                    Address = new Address
                    {
                        Id = 108,
                        Street = "246 Skin Care Road",
                        City = "White Harbor",
                        State = "WA",
                        ZipCode = "98101"
                    }
                },
                new Vet
                {
                    Id = 9,
                    FirstName = "Harvey",
                    LastName = "Roxie",
                    Specialty = "Ophthalmology",
                    AddressId = 109,
                    Address = new Address
                    {
                        Id = 109,
                        Street = "369 Vision Boulevard",
                        City = "Sunspear",
                        State = "AZ",
                        ZipCode = "85001"
                    }
                }
            };
        }
    }
}
