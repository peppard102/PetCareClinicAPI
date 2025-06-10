using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareClinicAPI.Data;
using PetCareClinicAPI.Models.Domain;
using PetCareClinicAPI.Models.DTO.Pets;

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

        [HttpGet("dropdown")]
        public async Task<ActionResult<List<VetDropdownDto>>> GetVetsForDropdown()
        {
            var vets = await _dbContext.Vets
                .Select(v => new VetDropdownDto
                {
                    Id = v.Id,
                    FullName = v.FirstName + " " + v.LastName
                })
                .ToListAsync();

            return Ok(vets);
        }
    }
}
