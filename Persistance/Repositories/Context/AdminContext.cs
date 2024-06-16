using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infastructure.Repositories.Context
{
    public class AdminContext : DbContext
    {
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        public AdminContext(DbContextOptions options) : base(options)
        {

        }

        /*  protected override void OnModelCreating(ModelBuilder modelBuilder)
          {

          }*/

    }
}
