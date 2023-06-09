using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class HabitatPlantConfiguration : EntityConfiguration<HabitatPlant>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<HabitatPlant> builder)
        {
            builder.HasKey(x => new { x.PlantId, x.HabitatId });
        }
    }
}
