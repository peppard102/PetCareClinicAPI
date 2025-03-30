﻿namespace PetCareClinicAPI.Data.Models
{
    public class Vet
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public int Age { get; set; }
    }
}