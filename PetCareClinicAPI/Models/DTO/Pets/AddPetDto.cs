using PetCareClinicAPI.Models.Domain;

namespace PetCareClinicAPI.Models.DTO.Pets
{
    public class AddPetDto
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public required string Street { get; set; }

        public string ApartmentNumber { get; set; } = string.Empty;

        public required string City { get; set; }

        public required string State { get; set; }

        public required string ZipCode { get; set; }
    }
}
