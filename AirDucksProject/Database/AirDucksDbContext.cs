using AirDucksProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AirDucksProject.Database
{
    public class AirDucksDbContext : DbContext
    {
        //DB configuration - connects to a database through a connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AirDucksDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }
        //DbSets
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Measurement> Measurements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Composite key configuration
            modelBuilder.Entity<Measurement>()
                .HasKey(m => new { m.TimeStamp, m.SensorId });

            //Configures the Mac coloumn in the Sensor DB table to require unique data
            modelBuilder.Entity<Sensor>().HasIndex(s => s.Mac).IsUnique();
        }
    }
}
