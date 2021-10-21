using eComSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
namespace eComSolution.Data.Configurations
{
    public class ResetPassConfiguration : IEntityTypeConfiguration<ResetPass>
    {
        public void Configure(EntityTypeBuilder<ResetPass> builder)
        {
            builder.ToTable("ResetPasses");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Token).IsRequired();
            builder.Property(x => x.Time).IsRequired();
            builder.Property(x => x.Numcheck).IsRequired().HasDefaultValue(0);

        }
    }
}