using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NewDiplom.Models;

namespace NewDiplom.Models
{
    public class DiplomContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<Plurality> Plurality { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Right> Rights { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Zadachi> Zadachis { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }
        public DbSet<TaskDistribution> TaskDistributions { get; set; }
        public DbSet<TypeComment> TypeComments { get; set; }
        public DbSet<FileTask> FileTask { get; set; }


        public DiplomContext(DbContextOptions<DiplomContext> options)
            : base(options)
        {
            
            Database.EnsureCreated();
            // Database.EnsureDeleted();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskDistribution>()
                .HasOne(c => c.Zadachi)
                .WithMany(c =>c.TaskDistributions)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
