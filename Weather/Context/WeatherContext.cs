using Microsoft.EntityFrameworkCore;
using Weather.Domain;

namespace Weather.Context
{
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions options) : base(options) { }
        public DbSet<WeatherRecord> WeatherRecords { get; set; }
    }
}
