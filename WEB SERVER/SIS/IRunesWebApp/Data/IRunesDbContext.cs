namespace IRunesWebApp.Data
{
    using IRunesWebApp.Models;
    using Microsoft.EntityFrameworkCore;

    public class IRunesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<TrackAlbum> TracksAlbums { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=DESKTOP-3OS7LSS\SQLEXPRESS;Database=IRunesDatabase;Integrated Security=true")
                .UseLazyLoadingProxies();    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrackAlbum>().HasKey(x => new {x.TrackId, x.AlbumId });
        }
    }
}
