using Microsoft.EntityFrameworkCore;

namespace OrderService.Models.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }   
}