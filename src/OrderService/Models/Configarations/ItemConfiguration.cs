using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Models.Entities;

namespace OrderService.Models.Configarations;

public class ItemConfiguration : IEntityTypeConfiguration<ItemEntity>
{
    public void Configure(EntityTypeBuilder<ItemEntity> builder)
    {
        builder.HasKey(i => i.Id);

        builder.HasOne(i => i.Category)
               .WithMany(c => c.Items)
               .HasForeignKey(i => i.CategoryId);
    }
}