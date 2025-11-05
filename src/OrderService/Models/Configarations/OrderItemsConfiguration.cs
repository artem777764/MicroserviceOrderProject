using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Models.Entities;

namespace OrderService.Models.Configarations;

public class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItemsEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemsEntity> builder)
    {
        builder.HasKey(i => new { i.OrderId, i.ItemId });

        builder.HasOne(oi => oi.Item)
               .WithMany(i => i.OrderItems)
               .HasForeignKey(oi => oi.ItemId);

        builder.HasOne(oi => oi.Order)
               .WithMany(o => o.OrderItems)
               .HasForeignKey(oi => oi.OrderId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}