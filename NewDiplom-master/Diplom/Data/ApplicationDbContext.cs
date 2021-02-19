using Diplom.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Diplom.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        //public DbSet<Function> Functions { get; set; }
        public DbSet<Plurality> Plurality { get; set; }
        public DbSet<Position> Positions { get; set; }
        //public DbSet<Right> Rights { get; set; }
        //public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Zadachi> Zadachis { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<TaskDistribution> TaskDistributions { get; set; }
        public DbSet<TypeComment> TypeComments { get; set; }
        public DbSet<FileTask> FileTask { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            // Database.EnsureDeleted();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TaskDistribution>()
                .HasOne(c => c.Zadachi)
                .WithMany(c => c.TaskDistributions)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
