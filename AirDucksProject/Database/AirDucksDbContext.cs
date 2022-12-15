using AirDucksProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AirDucksProject.Database
{
    public class AirDucksDbContext : DbContext
    {
        //DB configuration - connects to a database through a connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:weavershcool.database.windows.net,1433;Initial Catalog=AirducksDB;Persist Security Info=False;User ID=weaver1066;Password=Hastings!3879;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
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
