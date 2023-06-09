using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class CartPlantConfiguration : EntityConfiguration<CartPlant>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<CartPlant> builder)
        {
            builder.Property(x => x.Quantity).IsRequired();
            builder.HasKey(x => new { x.PlantId, x.CartId });
        }
    }
}
