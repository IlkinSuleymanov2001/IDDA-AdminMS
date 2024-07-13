using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.Repositories.Context
{
    public class AdminContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Organization> Organizations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>()
                .HasQueryFilter(c => c.Active == false);
        }
    }
}
