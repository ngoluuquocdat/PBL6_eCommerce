using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eComSolution.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.ShopId).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.DateCreated).IsRequired();
            builder.Property(x => x.OriginalPrice).IsRequired();
            builder.Property(x => x.ViewCount).IsRequired().HasDefaultValue(0);
            builder.Property(x => x.IsDeleted).IsRequired().HasDefaultValue(false);
                
            
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId);
            builder.HasOne(x => x.Shop).WithMany(x => x.Products).HasForeignKey(x => x.ShopId);
        }
    }
}