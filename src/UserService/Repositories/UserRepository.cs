using System.Threading.Tasks;
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
}