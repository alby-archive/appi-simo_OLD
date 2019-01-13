namespace AppiSimo.Data
{
    using Microsoft.EntityFrameworkCore;
    using Shared.Model;

    public class KingRogerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<Heat> Heats { get; set; }
        public DbSet<Light> Lights { get; set; }
        public DbSet<Rate> Rates { get; set; }

        public KingRogerContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEvent>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UsersEvents)
                .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<UserEvent>()
                .HasOne(bc => bc.Event)
                .WithMany(c => c.UsersEvents)
                .HasForeignKey(bc => bc.EventId);            
            
            modelBuilder.Entity<CourtRate>()
                .HasOne(bc => bc.Rate)
                .WithMany(b => b.CourtsRates)
                .HasForeignKey(bc => bc.RateId);

            modelBuilder.Entity<CourtRate>()
                .HasOne(bc => bc.Court)
                .WithMany(c => c.CourtsRates)
                .HasForeignKey(bc => bc.CourtId);

            modelBuilder.Entity<Court>()
                .Property(e => e.Id)
                .ValueGeneratedNever();
        }
    }
}