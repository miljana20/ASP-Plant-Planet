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
    public class DurabilityConfiguration : EntityConfiguration<Durability>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Durability> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasMany(x => x.Plants)
                   .WithOne(x => x.Durability)
                   .HasForeignKey(x => x.DurabilityId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
