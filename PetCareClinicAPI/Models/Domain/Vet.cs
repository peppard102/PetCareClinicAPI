namespace PetCareClinicAPI.Models.Domain
{
    public class Vet
    {
        public Guid Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public Guid AddressId { get; set; }

        public required string Specialty { get; set; }

        public required Address Address { get; set; }
        
        // Many-to-many: A vet can see multiple pets
        public List<PetVet> PetVets { get; set; } = new();
    }
}