﻿namespace PetCareClinicAPI.Models.Domain
{
    public class Address
    {
        public int Id { get; set; }

        public required string Street { get; set; }

        public string? ApartmentNumber { get; set; }

        public required string City { get; set; }

        public required string State { get; set; }

        public required string ZipCode { get; set; }
    }
}