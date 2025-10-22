using UserService.Models.Context;
using UserService.Models.Entities;
using UserService.Repositories.Interfaces;

namespace UserService.Repositories;

public class UserDataRepository : IUserDataRepository
{
    private readonly ApplicationDbContext _context;

    public UserDataRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Guid> CreateUserDataAsync(UserDataEntity userDataEntity)
    {
        await _context.UsersData.AddAsync(userDataEntity);
        await _context.SaveChangesAsync();
        return userDataEntity.Id;
    }
}