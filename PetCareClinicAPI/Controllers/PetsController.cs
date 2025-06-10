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
        public async Task<ActionResult<IEnumerable<Pet>>> Get()
        {
            var pets = await _dbContext.Pets
                .Include(p => p.Address)
                .ToListAsync();

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
        public async Task<ActionResult<int>> Create([FromBody] AddPetDto pet)
        {
            // Match existing address so we don't add duplicates
            var existingAddress = await _dbContext.Addresses.FirstOrDefaultAsync(a =>
                a.Street.ToLower().Trim() == pet.Street.ToLower().Trim() &&
                a.ApartmentNumber.ToLower().Trim() == pet.ApartmentNumber.ToLower().Trim() &&
                a.City.ToLower().Trim() == pet.City.ToLower().Trim() &&
                a.State.ToLower().Trim() == pet.State.ToLower().Trim() &&
                a.ZipCode.ToLower().Trim() == pet.ZipCode.ToLower().Trim()
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
                    Street = pet.Street,
                    ApartmentNumber = pet.ApartmentNumber,
                    City = pet.City,
                    State = pet.State,
                    ZipCode = pet.ZipCode
                };

                await _dbContext.Addresses.AddAsync(addressToUse);
                await _dbContext.SaveChangesAsync();
            }

            var petDomainModel = new Pet
            {
                FirstName = pet.FirstName,
                LastName = pet.LastName,
                DateOfBirth = pet.DateOfBirth,
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
