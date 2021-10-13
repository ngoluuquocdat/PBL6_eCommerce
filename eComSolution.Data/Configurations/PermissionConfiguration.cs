using eComSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
namespace eComSolution.Data.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(per => new { per.FunctionId, per.GroupId }); // có 2 keys

            builder.HasOne(x => x.Function).WithMany(x => x.Permissions).HasForeignKey(x => x.FunctionId);
            builder.HasOne(x => x.Group).WithMany(x => x.Permissions).HasForeignKey(x => x.GroupId);
        }
    }
}