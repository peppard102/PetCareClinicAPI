namespace PetCareClinicAPI.Models.Domain
{
    public class Pet
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public DateOnly DoB { get; set; }

        public int AddressId { get; set; }

        public required Address Address { get; set; }

        // Many-to-many: A pet can have multiple owners
        public List<PetOwner> PetOwners { get; set; } = new();

        // Many-to-many: A pet can see multiple vets
        public List<PetVet> PetVets { get; set; } = new();
    }
}