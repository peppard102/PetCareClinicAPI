namespace PetCareClinicAPI.Models.Domain
{
    public class PetVet
    {
        public int PetId { get; set; }

        public int VetId { get; set; }

        public required Pet Pet { get; set; }

        public required Vet Vet { get; set; }
    }
}
