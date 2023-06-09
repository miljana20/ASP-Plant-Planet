using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class HabitatConfiguration : EntityConfiguration<Habitat>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Habitat> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasMany(x => x.HabitatPlants)
                   .WithOne(x => x.Habitat)
                   .HasForeignKey(x => x.HabitatId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
