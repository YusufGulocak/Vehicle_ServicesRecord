using App.Repositories.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Services
{
    public class ServiceRecordConfugration:IEntityTypeConfiguration<ServiceRecord>
    {
        public void Configure(EntityTypeBuilder<ServiceRecord> builder)
        {
            builder.ToTable("Servicess");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.ServiceDate)
                .IsRequired();

            builder.Property(s => s.ProcessedBy)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Cost)
                .HasColumnType("decimal(18,2)");

            builder.Property(s => s.Explanation)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(s => s.TechnicianName)
                .HasMaxLength(100);

            builder.Property(s => s.VehicleId).IsRequired();
               
              
        }
    }
}
