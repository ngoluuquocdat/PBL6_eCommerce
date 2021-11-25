using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eComSolution.Data.Configurations
{
    public class HistoryConfiguration : IEntityTypeConfiguration<History>
    {
        public void Configure(EntityTypeBuilder<History> builder)
        {
            builder.ToTable("Histories");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Date).IsRequired();

            // 1-n: product - histories
            builder.HasOne(h => h.Product)
            .WithMany(p => p.Histories)
            .HasForeignKey(h => h.ProductId);

            // 1-n: user - histories
            builder.HasOne(h => h.User)
            .WithMany(u => u.Histories)
            .HasForeignKey(h => h.UserId);
        }
    }
}