using Application.Common.Pipelines.Transaction;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.Repositories.Context
{
    public class AdminContext : DbContext
    {
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Organization> Organizations { get; set; }

        public AdminContext(DbContextOptions options) : base(options)
        {

        }

        /*  protected override void OnModelCreating(ModelBuilder modelBuilder)
          {

          }*/

    }
}
