﻿using AirDucksProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AirDucksProject.Database
{
    public class AirDucksDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AirDucksDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Measurement> Measurements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Composite key configuration
            modelBuilder.Entity<Measurement>()
                .HasKey(m => new { m.TimeStamp, m.SensorId });
        }
    }
}
