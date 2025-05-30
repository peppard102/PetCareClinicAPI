using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareClinicAPI.Data;
using PetCareClinicAPI.Models.Domain;
using System.Collections.Generic;

namespace PetCareClinicAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ILogger<PetsController> _logger;
        private readonly PetCareClinicDbContext _dbContext;

        public PetsController(ILogger<PetsController> logger, PetCareClinicDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pet>> Get()
        {
            var pets = _dbContext.Pets
                .Include(p => p.Address)
                .ToList();
            return Ok(pets);
        }
    }
}
