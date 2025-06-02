namespace PetCareClinicAPI.Models.Domain
{
    public class Vet
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public int AddressId { get; set; }

        public required string Specialty { get; set; }

        public Address? Address { get; set; }
        
        // Many-to-many: A vet can see multiple pets
        public List<PetVet> PetVets { get; set; } = new();
    }
}