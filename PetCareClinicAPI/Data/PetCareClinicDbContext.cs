using Microsoft.EntityFrameworkCore;
using PetCareClinicAPI.Models.Domain;

namespace PetCareClinicAPI.Data
{
    public class PetCareClinicDbContext : DbContext
    {
        public PetCareClinicDbContext(DbContextOptions<PetCareClinicDbContext> options) : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Vet> Vets { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<PetOwner> PetOwners { get; set; }
        public DbSet<PetVet> PetVets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the many-to-many relationship between Pet and Owner
            modelBuilder.Entity<PetOwner>()
                .HasKey(po => new { po.PetId, po.OwnerId });

            modelBuilder.Entity<PetOwner>()
                .HasOne(po => po.Pet)
                .WithMany(p => p.PetOwners)
                .HasForeignKey(po => po.PetId);

            modelBuilder.Entity<PetOwner>()
                .HasOne(po => po.Owner)
                .WithMany(o => o.PetOwners)
                .HasForeignKey(po => po.OwnerId);

            // Configure the many-to-many relationship between Pet and Vet
            modelBuilder.Entity<PetVet>()
                .HasKey(pv => new { pv.PetId, pv.VetId });

            modelBuilder.Entity<PetVet>()
                .HasOne(pv => pv.Pet)
                .WithMany(p => p.PetVets)
                .HasForeignKey(pv => pv.PetId);

            modelBuilder.Entity<PetVet>()
                .HasOne(pv => pv.Vet)
                .WithMany(v => v.PetVets)
                .HasForeignKey(pv => pv.VetId);

            // Configure one-to-many relationship for Address
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Address)
                .WithMany()
                .HasForeignKey(p => p.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Owner>()
                .HasOne(o => o.Address)
                .WithMany()
                .HasForeignKey(o => o.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<Vet>()
                .HasOne(v => v.Address)
                .WithMany()
                .HasForeignKey(v => v.AddressId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 