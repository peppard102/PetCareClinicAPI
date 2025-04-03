namespace PetCareClinicAPI.Data.Models
{
    public class Pet
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public int Age { get; set; }

        public required Address Address { get; set; }
    }
}