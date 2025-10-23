using UserService.Models.Entities;

namespace UserService.Repositories.Interfaces;

public interface IUserDataRepository
{
    Task<Guid> CreateUserDataAsync(UserDataEntity userDataEntity);
}