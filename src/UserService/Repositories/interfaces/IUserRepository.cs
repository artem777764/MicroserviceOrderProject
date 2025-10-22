using UserService.Models.Entities;

namespace UserService.Repositories.interfaces;

public interface IUserRepository
{
    Task<Guid> CreateUserAsync(UserEntity userEntity);
}