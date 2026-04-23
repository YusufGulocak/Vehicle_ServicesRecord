using App.Repositories.Service;
using App.Repositories.Services;
using App.Repositories.User;
using App.Repositories.Vehicles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Vehicle> Vehicles { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ServiceRecord>()
                .Property(s => s.Cost)
                .HasPrecision(18, 2); // 18 basamak, 2 ondalık

            modelBuilder.Entity<Users>(entity => {
                entity.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
            });



        }

        public DbSet<ServiceRecord> ServiceRecords { get; set; } = default!;
        public DbSet<Users> Users { get; set; } = default!;
       

       
    }
    
}
