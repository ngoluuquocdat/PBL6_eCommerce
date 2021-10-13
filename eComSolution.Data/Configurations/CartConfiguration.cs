using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eComSolution.Data.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carts");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.ProductDetail_Id).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Price).IsRequired();

            // 1-n: ProductDetail - Carts
            builder.HasOne(cart => cart.ProductDetail)
            .WithMany(pd => pd.Carts)
            .HasForeignKey(cart => cart.ProductDetail_Id);

            // 1-n: User - Carts
            builder.HasOne(cart => cart.User)
            .WithMany(u => u.Carts)
            .HasForeignKey(cart => cart.UserId);

        }
    }

}