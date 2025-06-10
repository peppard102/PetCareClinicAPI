using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareClinicAPI.Data;
using PetCareClinicAPI.Models.Domain;
using PetCareClinicAPI.Models.DTO.Pets;
using System.Collections.Generic;
using System.Threading.Tasks;

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


        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Pet>> GetById([FromRoute] int id)
        {
            var pet = await _dbContext.Pets.FindAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            return Ok(pet);
        }

        [HttpGet("dropdown")]
        public async Task<ActionResult<List<PetDropdownDto>>> GetPetsForDropdown()
        {
            var pets = await _dbContext.Pets
                .Select(p => new PetDropdownDto
                {
                    Id = p.Id,
                    FullName = p.FirstName + " " + p.LastName
                })
                .ToListAsync();

            return Ok(pets);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] AddPetDto p)
        {
            // Match existing address so we don't add duplicates
            var existingAddress = await _dbContext.Addresses.FirstOrDefaultAsync(a =>
                a.Street.ToLower().Trim() == p.Street.ToLower().Trim() &&
                a.ApartmentNumber.ToLower().Trim() == p.ApartmentNumber.ToLower().Trim() &&
                a.City.ToLower().Trim() == p.City.ToLower().Trim() &&
                a.State.ToLower().Trim() == p.State.ToLower().Trim() &&
                a.ZipCode.ToLower().Trim() == p.ZipCode.ToLower().Trim()
            );

            Address addressToUse;

            if (existingAddress != null)
            {
                addressToUse = existingAddress;
            }
            else
            {
                // If no existing address, create a new one
                addressToUse = new Address
                {
                    Street = p.Street,
                    ApartmentNumber = p.ApartmentNumber,
                    City = p.City,
                    State = p.State,
                    ZipCode = p.ZipCode
                };

                await _dbContext.Addresses.AddAsync(addressToUse);
                await _dbContext.SaveChangesAsync();
            }

            var petDomainModel = new Pet
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                AddressId = addressToUse.Id,
            };

            await _dbContext.Pets.AddAsync(petDomainModel);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = petDomainModel.Id },
                petDomainModel.Id
            );
        }
    }
}
