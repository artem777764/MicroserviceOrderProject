using UserService.Models.Context;
using UserService.Models.Entities;
using UserService.Repositories.Interfaces;

namespace UserService.Repositories;

public class UserRoleRepository : IUserRoleRepository
{
    private readonly ApplicationDbContext _context;

    public UserRoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AppointRoleAsync(UserRoleEntity userRoleEntity)
    {
        await _context.UserRoles.AddAsync(userRoleEntity);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveRoleAsync(UserRoleEntity userRoleEntity)
    {
        _context.UserRoles.Remove(userRoleEntity);
        await _context.SaveChangesAsync();
    }
}