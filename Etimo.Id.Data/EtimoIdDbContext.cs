using Etimo.Id.Entities;
using Microsoft.EntityFrameworkCore;

namespace Etimo.Id.Data
{
    public class EtimoIdDbContext : DbContext
    {
        public EtimoIdDbContext(DbContextOptions<EtimoIdDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>().HasIndex(user => user.ClientId).IsUnique();
            modelBuilder.Entity<User>().HasIndex(user => user.Username).IsUnique();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
