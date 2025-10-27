using Microsoft.EntityFrameworkCore;
using OrderService.Models.Configarations;
using OrderService.Models.Entities;

namespace OrderService.Models.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ItemConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemsConfiguration());
        modelBuilder.ApplyConfiguration(new StatusConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ItemEntity> Items { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderItemsEntity> OrderItems { get; set; }
    public DbSet<StatusEntity> Statuses { get; set; }
}