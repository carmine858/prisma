using Microsoft.EntityFrameworkCore;
using Prisma.Models;

namespace Prisma.Data
{
    public class PrismaDbContext : DbContext
    {
        public PrismaDbContext(DbContextOptions<PrismaDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurazione dei modelli
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}