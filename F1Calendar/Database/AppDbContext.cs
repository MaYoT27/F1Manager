using Microsoft.EntityFrameworkCore;
using F1Manager.Entities;

namespace F1Manager.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<DriverEntity> Drivers { get; set; }
        public DbSet<RaceEntity> Races { get; set; }

        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}