using Microsoft.EntityFrameworkCore;
using WebMusic.Models;

namespace WebMusic.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Banda> Bandas { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Cancion> Cancions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genero>()
                .HasIndex(t => t.des_genero)
                .IsUnique();

            modelBuilder.Entity<Banda>()
                .HasIndex(t => t.nombre)
                .IsUnique();

            modelBuilder.Entity<Album>()
                .HasIndex(t => t.nombre)
                .IsUnique();

            modelBuilder.Entity<Cancion>()
                .HasIndex(t => t.Nombre)
                .IsUnique();
        }
    }
}
