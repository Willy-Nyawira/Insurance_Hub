using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using InsuranceHub.Domain.Entities;
using InsuranceHub.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace InsuranceHub.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Policy> Policies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Email as a value object
            modelBuilder.Entity<User>()
                .OwnsOne(u => u.Email, a =>
                {
                    a.Property(e => e.Address).HasColumnName("EmailAddress");
                });
            modelBuilder.Entity<User>()
                .OwnsOne(u => u.Credentials);
            modelBuilder.Entity<Role>()
           .Property(r => r.RoleType)
           .HasConversion(
               v => v.ToString(),         
               v => (UserRole)Enum.Parse(typeof(UserRole), v) 
           );

            modelBuilder.Entity<Role>().HasData(
         new Role { Id = Guid.NewGuid(), RoleType = UserRole.User },
         new Role { Id = Guid.NewGuid(), RoleType = UserRole.Admin },
         new Role { Id = Guid.NewGuid(), RoleType = UserRole.Supervisor },
         new Role { Id = Guid.NewGuid(), RoleType = UserRole.Customer }
     );
            modelBuilder.Entity<Customer>()
            .OwnsOne(c => c.EmailAddress, sa =>
            {
                sa.Property(e => e.Address).HasColumnName("EmailAddress");
            });
            modelBuilder.Entity<Policy>()
        .HasOne(p => p.User)
        .WithMany(u => u.Policies)
        .HasForeignKey(p => p.UserId);
        }
    }
}

