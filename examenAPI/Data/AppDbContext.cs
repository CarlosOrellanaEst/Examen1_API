using Microsoft.EntityFrameworkCore;
using examenAPI.Models;

namespace examenAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
    /*
            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Email)
                .IsUnique(); // Ensure Email is unique

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Phone)
                .IsUnique(); // Ensure Phone is unique
        }
        */
        }
    }
}
