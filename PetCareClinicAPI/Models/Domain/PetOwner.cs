namespace PetCareClinicAPI.Models.Domain
{
    public class PetOwner
    {
        public Guid PetId { get; set; }

        public Guid OwnerId { get; set; }

        public required Pet Pet { get; set; }

        public required Owner Owner { get; set; }
    }
}
