using Microsoft.EntityFrameworkCore;
using UserService.Models.Configurations;
using UserService.Models.Entities;

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
    
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserDataEntity> UsersData { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserRoleEntity> UserRoles { get; set; }
}