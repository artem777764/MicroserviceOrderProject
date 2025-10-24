using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserService.Models.Context;
using UserService.Models.Entities;
using UserService.Repositories.Interfaces;

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
        return await BuildDefectsQuery().FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        return await BuildDefectsQuery().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<UserEntity?> GetByLoginAsync(string login)
    {
        return await BuildDefectsQuery().FirstOrDefaultAsync(u => u.Login == login);
    }

    public async Task<List<UserEntity>> GetAllAsync()
    {
        return await BuildDefectsQuery().ToListAsync();
    }

    public async Task<bool> IsEmailExist(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> IsLoginExist(string login)
    {
        return await _context.Users.AnyAsync(u => u.Login == login);
    }

    private IQueryable<UserEntity> BuildDefectsQuery()
    {
        return _context.Users.Include(u => u.UserData)
                             .Include(u => u.UserRoles)
                             .ThenInclude(ur => ur.Role)
                             .AsNoTracking();
    }
}