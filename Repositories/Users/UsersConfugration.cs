using App.Repositories.Vehicles;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.User
{
    public class UsersConfugration
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.UserName).IsRequired().HasMaxLength(150);
            builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Role).IsRequired().HasMaxLength(20);
           
        }
    }
}
