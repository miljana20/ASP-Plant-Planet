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
    public class CartConfiguration : EntityConfiguration<Cart>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<Cart> builder)
        {
            builder.HasOne(x => x.User)
                   .WithMany(x => x.Carts)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Status).HasDefaultValue(false);
            builder.HasMany(x => x.CartPlants)
                   .WithOne(x => x.Cart)
                   .HasForeignKey(x => x.CartId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
