using Microsoft.EntityFrameworkCore;
using ReceptekWebAPI.Entities;

namespace ReceptekWebAPI.Data
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Recept> Receptek { get; set; } = null!;
        public DbSet<Cimke> Cimkek { get; set; } = null!;
        public DbSet<ReceptCimke> ReceptCimkek { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Receptek)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReceptCimke>()
        .HasKey(rc => new { rc.ReceptId, rc.CimkeId });

            modelBuilder.Entity<ReceptCimke>()
                .HasOne(rc => rc.Recept)
                .WithMany(r => r.ReceptCimkek)
                .HasForeignKey(rc => rc.ReceptId);

            modelBuilder.Entity<ReceptCimke>()
                .HasOne(rc => rc.Cimke)
                .WithMany(c => c.ReceptCimkek)
                .HasForeignKey(rc => rc.CimkeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
