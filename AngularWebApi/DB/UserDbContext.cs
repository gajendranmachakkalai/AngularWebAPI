using AngularWebApi.Model;
using Dp.Lll.Infrastrucutre.Model.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Dp.Lll.Infrastrucutre.Model
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.EmailId)
                .IsUnique();

            modelBuilder.SeedMaster();
        }

        #region User Module

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }


        #endregion

    }
}
