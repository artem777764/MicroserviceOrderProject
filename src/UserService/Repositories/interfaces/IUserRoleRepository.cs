using UserService.Models.Entities;

namespace UserService.Repositories.Interfaces;

public interface IUserRoleRepository
{
    Task AppointRoleAsync(UserRoleEntity userRoleEntity);
    Task RemoveRoleAsync(UserRoleEntity userRoleEntity);
}