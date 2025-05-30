using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareClinicAPI.Data;
using PetCareClinicAPI.Models.Domain;

namespace PetCareClinicAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VetsController : ControllerBase
    {
        private readonly ILogger<VetsController> _logger;
        private readonly PetCareClinicDbContext _dbContext;

        public VetsController(ILogger<VetsController> logger, PetCareClinicDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vet>> Get()
        {
            var vets = _dbContext.Vets
                .Include(v => v.Address)
                .ToList();

            return Ok(vets);
        }
    }
}
