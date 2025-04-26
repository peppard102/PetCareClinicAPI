using Microsoft.AspNetCore.Mvc;
using PetCareClinicAPI.Data.Models;

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
                new Vet{ Id = 1, LastName = "Snow", FirstName = "Jon", Age = 35 },
                new Vet{ Id = 2, LastName = "Lannister", FirstName = "Cersei", Age = 42 },
                new Vet{ Id = 3, LastName = "Lannister", FirstName = "Jaime", Age = 45 },
                new Vet{ Id = 4, LastName = "Stark", FirstName = "Arya", Age = 16 },
                new Vet{ Id = 5, LastName = "Targaryen", FirstName = "Daenerys", Age = 5 },
                new Vet{ Id = 6, LastName = "Melisandre", FirstName = "Samantha", Age = 150 },
                new Vet{ Id = 7, LastName = "Clifford", FirstName = "Ferrara", Age = 44 },
                new Vet{ Id = 8, LastName = "Frances", FirstName = "Rossini", Age = 36 },
                new Vet{ Id = 9, LastName = "Roxie", FirstName = "Harvey", Age = 65 }
            };
        }
    }
}
