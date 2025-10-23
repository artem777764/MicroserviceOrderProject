using Backend.DTOs.UserDTOs;
using UserService.Models.Entities;

namespace UserService.Extensions;

public static class RoleExtensions
{
    public static GetRoleDTO ToGetDTO(this RoleEntity roleEntity) => new GetRoleDTO()
    {
        Id = roleEntity.Id,
        Name = roleEntity.Name,
    };
}