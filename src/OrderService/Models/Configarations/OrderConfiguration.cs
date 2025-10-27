using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Models.Entities;

namespace OrderService.Models.Configarations;

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> builder)
    {
        builder.HasKey(o => o.Id);

        builder.HasOne(o => o.Status)
               .WithMany(s => s.Orders)
               .HasForeignKey(o => o.StatusId);
    }
}