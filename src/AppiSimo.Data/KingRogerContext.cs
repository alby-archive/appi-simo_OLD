namespace AppiSimo.Data
{
    using Microsoft.EntityFrameworkCore;
    using Shared.Model;

    public class KingRogerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Court> Courts { get; set; }

        public KingRogerContext(DbContextOptions options) : base(options)
        {
        }
    }
}