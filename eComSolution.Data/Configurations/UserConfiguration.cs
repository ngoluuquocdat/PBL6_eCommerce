using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eComSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace eComSolution.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Username).IsRequired().HasMaxLength(30);
            builder.Property(x => x.PasswordHash).IsRequired(); 
            builder.Property(x => x.PasswordSalt).IsRequired();    
            builder.Property(x => x.Fullname).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().IsUnicode().HasMaxLength(62);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(15);

            // 1-1: User - Shop
            builder.HasOne(u => u.Shop)
                .WithOne(sh => sh.User)
                .HasForeignKey<User>(u => u.ShopId);
        }


    }
}