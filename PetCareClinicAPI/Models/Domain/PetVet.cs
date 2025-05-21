namespace PetCareClinicAPI.Models.Domain
{
    public class PetVet
    {
        public Guid PetId { get; set; }

        public Guid VetId { get; set; }

        public required Pet Pet { get; set; }

        public required Vet Vet { get; set; }
    }
}
