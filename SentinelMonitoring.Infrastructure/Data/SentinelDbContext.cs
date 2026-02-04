using Microsoft.EntityFrameworkCore;
using SentinelMonitoring.Core.Models;

namespace SentinelMonitoring.Infrastructure.Data
{
    public class SentinelDbContext : DbContext
    {
        public SentinelDbContext(DbContextOptions<SentinelDbContext> options) : base(options) { }

        public DbSet<SensorData> Telemetries { get; set; }
        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SensorData>().HasKey(s => s.Id);
            modelBuilder.Entity<Device>().HasKey(d => d.Id);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
