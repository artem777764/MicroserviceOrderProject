using UserService.Models.Entities;

namespace UserService.Repositories.Interfaces;

public interface IUserRepository
{
    Task<Guid> CreateUserAsync(UserEntity userEntity);
    Task<List<UserEntity>> GetAllAsync();
    Task<UserEntity?> GetByEmailAsync(string email);
    Task<UserEntity?> GetByIdAsync(Guid userId);
    Task<UserEntity?> GetByLoginAsync(string login);
    Task<bool> IsEmailExist(string email);
    Task<bool> IsLoginExist(string login);
    Task RemoveByIdAsync(UserEntity userEntity);
}
