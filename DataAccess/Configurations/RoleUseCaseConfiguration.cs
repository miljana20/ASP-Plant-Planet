using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class RoleUseCaseConfiguration : EntityConfiguration<RoleUseCase>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<RoleUseCase> builder)
        {
        }
    }
}
