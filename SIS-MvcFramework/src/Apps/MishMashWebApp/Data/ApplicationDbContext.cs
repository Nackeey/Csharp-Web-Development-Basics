namespace MishMashWebApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using MishMashWebApp.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
       
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Tag> Tag { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-3OS7LSS\\SQLEXPRESS;Database=MishMash;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
