namespace PetCareClinicAPI.Models.Domain
{
    public class PetOwner
    {
        public int PetId { get; set; }

        public int OwnerId { get; set; }

        public required Pet Pet { get; set; }

        public required Owner Owner { get; set; }
    }
}
