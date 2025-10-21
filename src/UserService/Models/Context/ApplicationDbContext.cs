using Microsoft.EntityFrameworkCore;
using UserService.Models.Configurations;

namespace UserService.Models.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserDataConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}