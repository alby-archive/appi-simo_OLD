namespace AppiSimo.Data
{
    using Microsoft.EntityFrameworkCore;
    using Shared.Model;

    public class KingRogerContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public KingRogerContext(DbContextOptions options) : base(options)
        {
        }
    }
}