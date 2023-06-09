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
    public class PlantConfiguration : EntityConfiguration<Plant>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Plant> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Image).IsRequired();
            builder.HasMany(x => x.HabitatPlants)
                   .WithOne(x => x.Plant)
                   .HasForeignKey(x => x.PlantId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.CartPlants)
                   .WithOne(x => x.Plant)
                   .HasForeignKey(x => x.PlantId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
