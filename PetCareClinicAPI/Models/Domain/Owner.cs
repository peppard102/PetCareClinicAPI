namespace PetCareClinicAPI.Models.Domain
{
    public class Owner
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public int AddressId { get; set; }

        public required Address Address { get; set; }
        
        // Many-to-many: An owner can have multiple pets
        public List<PetOwner> PetOwners { get; set; } = new();
    }
}