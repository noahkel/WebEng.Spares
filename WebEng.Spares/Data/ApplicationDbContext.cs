using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebEng.ReplacementParts.Data;
using WebEng.ReplacementParts.Models;

namespace WebEng.ReplacementParts.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(eb =>
            {
                eb.HasKey(e => e.Name);
                eb.Property(e => e.Description).HasColumnName("CarDescription");
                eb.Property(e => e.PictureUrl);
            });

            modelBuilder.Entity<Manufacturer>(eb =>
            {
                eb.HasKey(e => e.Name);
                eb.Property(e => e.Description);
                eb.Property(e => e.PictureUrl);
            });

            modelBuilder.Entity<Car>(eb =>
            {
                eb.HasKey(eb => eb.Key);
                eb.Property(eb => eb.Name);
                eb.Property(eb => eb.Description);
                eb.Property(eb => eb.Weight);
                eb.Property(eb => eb.PictureUrl);
                eb.Property(eb => eb.Finished).HasColumnType("Date");
                eb.Property(eb => eb.Started).HasColumnType("Date");
                eb.HasOne(eb => eb.Brand).WithMany().HasForeignKey(d => d.BrandFK);
            });

            modelBuilder.Entity<ReplacementPart>(eb =>
            {
                eb.HasKey(e => e.Key);
                eb.Property(e => e.Name);
                eb.Property(e => e.Description);
                eb.Property(e => e.Price);
                eb.Property(e => e.Available);
                eb.Property(e => e.Weight);
                eb.Property(e => e.PictureUrl);
                eb.HasOne(e => e.Manufacturer).WithMany().HasForeignKey(fk => fk.ManufacturerKey);
                eb.HasOne(e => e.OEM).WithMany().HasForeignKey(fk => fk.OEMKey);
            });

            modelBuilder.Entity<OEMCar>(eb =>
            {
                eb.HasKey(e => e.Key);
                eb.HasOne(e => e.Car).WithMany().HasForeignKey(fk => fk.CarFK);
                eb.HasOne(e => e.OEM).WithMany().HasForeignKey(fk => fk.OEMFK);
            });

            

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<WebEng.ReplacementParts.Data.Brand> Brand { get; set; }
        public DbSet<WebEng.ReplacementParts.Data.Car> Car { get; set; }
        public DbSet<WebEng.ReplacementParts.Models.OEM> OEM { get; set; }
        public DbSet<WebEng.ReplacementParts.Models.Manufacturer> Manufacturer { get; set; }
        public DbSet<WebEng.ReplacementParts.Models.ReplacementPart> ReplacementPart { get; set; }
        public DbSet<WebEng.ReplacementParts.Models.OEMCar> OEMCar { get; set; }
    }
}
