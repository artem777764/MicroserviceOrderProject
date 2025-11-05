using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Models.Entities;

namespace UserService.Models.Configurations;

public class UserDataConfiguration : IEntityTypeConfiguration<UserDataEntity>
{
    public void Configure(EntityTypeBuilder<UserDataEntity> builder)
    {
        builder.HasKey(ud => ud.Id);

        builder.HasOne(ud => ud.User)
               .WithOne(u => u.UserData)
               .HasForeignKey<UserDataEntity>(ud => ud.Id)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
    }
}