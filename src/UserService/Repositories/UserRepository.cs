using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserService.Models.Context;
using UserService.Models.Entities;
using UserService.Repositories.interfaces;

namespace UserService.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> CreateUserAsync(UserEntity userEntity)
    {
        await _context.Users.AddAsync(userEntity);
        await _context.SaveChangesAsync();
        return userEntity.Id;
    }

    public async Task<UserEntity?> GetByIdAsync(Guid userId)
    {
        return await _context.Users.Include(u => u.UserData)
                                   .Include(u => u.UserRoles)
                                   .ThenInclude(ur => ur.Role)
                                   .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<List<UserEntity>> GetAllAsync()
    {
        return _context.Users.Include(u => u.UserData)
                             .Include(u => u.UserRoles)
                             .ThenInclude(ur => ur.Role)
                             .ToList();
    }
}