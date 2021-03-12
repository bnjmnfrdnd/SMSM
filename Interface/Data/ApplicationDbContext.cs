using Interface.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Interface.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {            
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=SMSMIDB.db");
        }

        public DbSet<AudioBook> AudioBooks { get; set; } 
        public DbSet<Request> Requests { get; set; } 
        public DbSet<RequestUser> RequestUsers { get; set; } 
        public DbSet<RequestType> RequestTypes { get; set; } 
        public DbSet<Movie> Movies { get; set; } 
        public DbSet<TVShows> TVShows { get; set; } 
        public DbSet<TVShows> Notifications { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Movie>(entity =>
            {
                entity.Property(i => i.ID).UseIdentityColumn();
                entity.Property(i => i.Title).IsRequired();
                entity.Property(i => i.YYYY).IsRequired();
            });

            builder.Entity<Request>(entity =>
            {
                entity.Property(i => i.ID).UseIdentityColumn();
                entity.Property(i => i.Title).IsRequired();
                entity.Property(i => i.Type).IsRequired();
                entity.Property(i => i.RequestedBy).IsRequired();
                entity.Property(i => i.IsComplete).HasDefaultValue(0);
                entity.Property(i => i.Acknowledged).HasDefaultValue(0);
            });
            
            builder.Entity<TVShows>(entity =>
            {
                entity.Property(i => i.ID).UseIdentityColumn();
                entity.Property(i => i.Title).IsRequired();
            });

            builder.Entity<RequestUser>(entity =>
            {
                entity.Property(i => i.ID).UseIdentityColumn();
                entity.Property(i => i.Name).IsRequired();
            });

            builder.Entity<RequestType>(entity =>
            {
                entity.Property(i => i.ID).UseIdentityColumn();
                entity.Property(i => i.Type).IsRequired();
            });

            builder.Entity<RequestType>().HasData(
            new RequestType
            {
                ID = -1,
                Type = "Movie",
                Enabled = true,
            });

            builder.Entity<RequestType>().HasData(
            new RequestType
            {
                ID = -2,
                Type = "TV Show",
                Enabled = true,
            });

            builder.Entity<RequestUser>().HasData(
            new RequestUser
            {
                ID = -1,
                Name = "Guest",
                Active = true,
            });

            base.OnModelCreating(builder);
        }
    }
}