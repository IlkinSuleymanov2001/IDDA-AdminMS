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
                .HasData(
                [
                    new Organization {Id = 1,Name = "ABB",Active = true},
                    new Organization {Id = 2,Name = "IDDA",Active = false},
                ]
                    );

            modelBuilder.Entity<Staff>()
                .HasData(
                    [ new Staff
                        {
                            Id = 1,
                            Fullname = "ilkin suleymanov ",
                            Username = "ilkinsuleymanov200@gmail.com",
                            OrganizationID = 1,
                            Active = true
                        }
                    ]
                );
            modelBuilder.Entity<Organization>()
                .HasQueryFilter(c => c.Active==true);

            modelBuilder.Entity<Staff>()
                .HasQueryFilter(c => c.Active == true);

        }
    }
}
