using eComSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
namespace eComSolution.Data.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.OrderId).IsRequired();
            builder.Property(x => x.ProductDetail_Id).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            
            // 1-n: order - orderDetails
            builder.HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId);

            // 1-n: productDetail - orderDetails
            builder.HasOne(od => od.ProductDetail)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(od => od.ProductDetail_Id)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}