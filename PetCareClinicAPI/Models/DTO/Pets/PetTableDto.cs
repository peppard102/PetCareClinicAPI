using PetCareClinicAPI.Models.Domain;

namespace PetCareClinicAPI.Models.DTO.Pets
{
    public class PetTableDto
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public required string Address { get; set; }
    }
}
