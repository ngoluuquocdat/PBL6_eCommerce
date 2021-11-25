using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eComSolution.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.OrderDate).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.ShopId).IsRequired();
            builder.Property(x => x.State).IsRequired();
            builder.Property(x => x.ShipAddress).IsRequired();
            builder.Property(x => x.ShipPhone).IsRequired().HasMaxLength(15);
            builder.Property(x => x.ShipName).IsRequired().HasMaxLength(50);

            // 1-n: user - orders
            builder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);

            // 1-n: shop - orders
            builder.HasOne(o => o.Shop)
            .WithMany(sh => sh.Orders)
            .HasForeignKey(o => o.ShopId);
        }
    }
}