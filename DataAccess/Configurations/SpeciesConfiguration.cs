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
    public class SpeciesConfiguration : EntityConfiguration<Species>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Species> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasMany(x => x.Plants)
                   .WithOne(x => x.Species)
                   .HasForeignKey(x => x.SpeciesId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.ChildrenSpecies)
                   .WithOne(x => x.ParentSpecies)
                   .HasForeignKey(x => x.ParentSpeciesId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
