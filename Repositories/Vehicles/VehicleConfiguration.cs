using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Vehicles;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
       builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Brand).IsRequired().HasMaxLength(150);
        builder.Property(x => x.NumberPlate).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Year).IsRequired();
    }
}
