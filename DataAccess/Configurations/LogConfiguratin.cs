﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    internal class LogConfiguratin : EntityConfiguration<Log>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Log> builder)
        {
        }
    }
}
